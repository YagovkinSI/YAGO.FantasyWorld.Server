using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Yago.FantasyWorld.ApiContracts.Common.Enums;
using Yago.FantasyWorld.ApiContracts.Domain;
using Yago.FantasyWorld.ApiContracts.QuestApi.Enums;
using Yago.FantasyWorld.ApiContracts.QuestApi.Models;
using YAGO.FantasyWorld.Server.Application.History;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base;
using YAGO.FantasyWorld.Server.Domain.Exceptions;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.YagoException;

namespace YAGO.FantasyWorld.Server.Application.Quests
{
    /// <summary>
    /// Сервис работы с квестами
    /// </summary>
    public class QuestService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IQuestDatabaseService _questDatabaseService;
        private readonly IDatabaseTransactionChangeService _databaseTransactionChangeService;
        private readonly OrganizationService _organizationService;
        private readonly HistoryService _historyService;
        private readonly QuestReadinessService _questReadinessService;
        private readonly QuestGenerator _questGenerator;

        public QuestService(IAuthorizationService authorizationService,
            IQuestDatabaseService questDatabaseService,
            OrganizationService organizationService,
            IDatabaseTransactionChangeService databaseTransactionChangeService,
            QuestGenerator questGenerator,
            HistoryService historyService,
            QuestReadinessService questReadinessService)
        {
            _authorizationService = authorizationService;
            _questDatabaseService = questDatabaseService;
            _organizationService = organizationService;
            _databaseTransactionChangeService = databaseTransactionChangeService;
            _questGenerator = questGenerator;
            _historyService = historyService;
            _questReadinessService = questReadinessService;
        }

        /// <summary>
        /// Получение квеста
        /// </summary>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Данные по квесту</returns>
        public async Task<QuestData> GetQuest(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!user.IsAuthorized)
                throw new ApplicationException("Для получения квеста необходимо авторизоваться");
            if (user.User.OrganizationId == null)
                throw new ApplicationException("Для получения квеста необходимо выбрать организацию");

            var lastQuests = await _questDatabaseService.GetLastQuestes(user.User.OrganizationId.Value, _questReadinessService.QuestTimeoutCount, cancellationToken);
            return await TryGetNotCompletedQuest(lastQuests, cancellationToken)
                ?? TryGetNotReadyQuestData(lastQuests, cancellationToken)
                ?? await GetNewQuest(user.User.OrganizationId.Value, lastQuests, cancellationToken);
        }

        private async Task<QuestData> GetNewQuest(long organizationId, IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var quest = await _questGenerator.GenerateQuest(organizationId, lastQuests, cancellationToken);
            var questWithDetails = await GetQuestWithDetails(quest, cancellationToken);
            return new QuestData(questWithDetails);
        }

        /// <summary>
        /// Установка варианта решения квеста
        /// </summary>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        /// <param name="questId">Идентификатор квеста</param>
        /// <param name="questOptionId">Идентификатор выбранного варианта</param>
        /// <param name="cancellationToken">ТОкен отмены</param>
        /// <returns>Результат квеста</returns>
        public async Task<string> SetQuestOption(ClaimsPrincipal claimsPrincipal, long questId, int questOptionId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!user.IsAuthorized)
                throw new ApplicationException("Для получения квеста необходимо авторизоваться");
            if (user.User.OrganizationId == null)
                throw new ApplicationException("Для получения квеста необходимо выбрать организацию");

            var quest = await _questDatabaseService.FindQuest(questId, cancellationToken);
            if (quest.OrganizationId != user.User.OrganizationId)
                throw new ApplicationException("Некорректный идентификатор квеста.");
            if (quest.Status != QuestStatus.Created)
                throw new ApplicationException("Неверный статус квеста.");

            var questDatails = GetQuestDatails(quest);
            var result = await questDatails.HandleQuestOption(questOptionId, cancellationToken);

            var historyEvent = await _historyService.CreateHistoryEvent(quest, questOptionId, result, cancellationToken);

            await _databaseTransactionChangeService.HandleTransactionChange(historyEvent, quest.Id, cancellationToken);
            return GetResultText(result, quest.OrganizationId);
        }

        private string GetResultText(QuestOptionResult questOptionResult, long organizationId)
        {
            var mainText = questOptionResult.Text;

            var organizationResult = questOptionResult.EntitiesChange
                .SingleOrDefault(r => r.EntityType == EntityType.Organization && r.EntityId == organizationId)
                ?.EntityParametersChange.SingleOrDefault(r => r.EntityParameter == EntityParameter.OrganizationPower)
                ?.Change;
            var organizationResultInt = organizationResult == null ? 0 : int.Parse(organizationResult);
            var organizationResultText = organizationResultInt != 0
                ? organizationResultInt >= 0
                    ? $"Ваше могущество увеличилсось на {organizationResultInt}"
                    : $"Ваше могущество уменьшилось на {-organizationResultInt}"
                : "Ваше могущество не изменилось";

            var oponnentResult = questOptionResult.EntitiesChange
                .SingleOrDefault(r => r.EntityType == EntityType.Organization && r.EntityId != organizationId)
                ?.EntityParametersChange.SingleOrDefault(r => r.EntityParameter == EntityParameter.OrganizationPower)
                ?.Change;
            var oponnentResultInt = oponnentResult == null ? 0 : int.Parse(oponnentResult);
            var oponnentResultText = oponnentResultInt != 0
                ? oponnentResultInt >= 0
                    ? $"Могущество оппонента увеличилсось на {oponnentResultInt}"
                    : $"Могущество оппонента уменьшилось на {-oponnentResultInt}"
                : string.Empty;

            return $"{mainText}\r\n{organizationResultText}{(string.IsNullOrEmpty(oponnentResultText) ? string.Empty : $"\r\n{oponnentResultText}")}";
        }

        private QuestData TryGetNotReadyQuestData(IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var questReadyDateTime = _questReadinessService.CalcQuestReadyDateTime(lastQuests.ToArray());
            return questReadyDateTime > DateTimeOffset.Now ? new QuestData(questReadyDateTime) : null;
        }

        private async Task<QuestData> TryGetNotCompletedQuest(IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notCompletedQuests = lastQuests.Where(q => q.Status == QuestStatus.Created);
            if (notCompletedQuests.Any())
            {
                var lastNotCompletedQuest = notCompletedQuests.Last();
                var lastQuestForUser = await GetQuestWithDetails(lastNotCompletedQuest, cancellationToken);
                return new QuestData(lastQuestForUser);
            }

            return null;
        }

        private IQuestDetailsProvider GetQuestDatails(Quest quest)
        {
            return quest.Type switch
            {
                QuestType.Unknown => throw new ApplicationException("Неизвестный тип квеста! Обратитесь к разработчику."),
                QuestType.BaseQuest => new BaseQuest(quest, _organizationService),
                _ => throw new YagoNotImplementedException(),
            };
        }

        private async Task<QuestWithDetails> GetQuestWithDetails(Quest quest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var questProvider = GetQuestDatails(quest);
            var questDetails = await questProvider.GetQuestForUser(quest, cancellationToken);
            return new QuestWithDetails(quest, questDetails);
        }
    }
}
