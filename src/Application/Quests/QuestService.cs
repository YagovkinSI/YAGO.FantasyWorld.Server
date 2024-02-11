using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Application.Quests.QuestsForUser;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Enums;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Application.Quests
{
    /// <summary>
    /// Сервис работы с квестами
    /// </summary>
    public class QuestService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IQuestDatabaseService _questDatabaseService;
        private readonly OrganizationService _organizationService;

        private readonly QuestForUserMapper _questForUserMapper;

        private readonly Random _random = new();

        private readonly TimeSpan[] QUEST_TIMEOUTS = new[]
        {
            TimeSpan.FromMinutes(2),
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(20),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromHours(3)
        };


        public QuestService(IAuthorizationService authorizationService,
            IQuestDatabaseService questDatabaseService,
            OrganizationService organizationService)
        {
            _authorizationService = authorizationService;
            _questDatabaseService = questDatabaseService;
            _organizationService = organizationService;

            _questForUserMapper = new QuestForUserMapper(_organizationService);
        }

        /// <summary>
        /// Получение квеста
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<QuestData> GetQuest(System.Security.Claims.ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!user.IsAuthorized)
                throw new ApplicationException("Для получения квеста необходимо авторизоваться");
            if (user.User.OrganizationId == null)
                throw new ApplicationException("Для получения квеста необходимо выбрать организацию");

            var lastQuests = await _questDatabaseService.GetLastQuestes(user.User.OrganizationId.Value, QUEST_TIMEOUTS.Length, cancellationToken);
            return await TryGetNotCompletedQuest(lastQuests, cancellationToken)
                ?? TryGetNotReadyQuestData(lastQuests, cancellationToken)
                ?? await GetNewQuest(user.User.OrganizationId.Value, lastQuests, cancellationToken);
        }

        private async Task<QuestData> GetNewQuest(long organizationId, IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var newQuest = await GenerateNewQuest(organizationId, lastQuests, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            newQuest = await _questDatabaseService.CreateQuest(newQuest, cancellationToken);
            var questForUser = await _questForUserMapper.GetQuestForUser(newQuest, cancellationToken);
            return new QuestData(questForUser);
        }

        private QuestData TryGetNotReadyQuestData(IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var questReadyDateTime = CalcQuestReadyDateTime(lastQuests.ToArray());
            return questReadyDateTime > DateTimeOffset.Now ? new QuestData(questReadyDateTime) : null;
        }

        private async Task<QuestData> TryGetNotCompletedQuest(IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var notCompletedQuests = lastQuests.Where(q => q.Status == QuestStatus.Created);
            if (notCompletedQuests.Any())
            {
                var lastNotCompletedQuest = notCompletedQuests.Last();
                var lastQuestForUser = await _questForUserMapper.GetQuestForUser(lastNotCompletedQuest, cancellationToken);
                return new QuestData(lastQuestForUser);
            }

            return null;
        }

        private DateTimeOffset CalcQuestReadyDateTime(Quest[] lastQuests)
        {
            if (lastQuests.Length < QUEST_TIMEOUTS.Length)
                return DateTimeOffset.MinValue;

            var questReadyDateTime = DateTimeOffset.Now + QUEST_TIMEOUTS[0];
            for (var i = 0; i < QUEST_TIMEOUTS.Length; i++)
            {
                var currentQuestReadyDateTime = lastQuests[0].Created + QUEST_TIMEOUTS[0];
                if (currentQuestReadyDateTime < questReadyDateTime)
                    questReadyDateTime = currentQuestReadyDateTime;
            }

            return questReadyDateTime;
        }

        private async Task<Quest> GenerateNewQuest(long organizationId, IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await GenarateBaseQuest(organizationId, lastQuests, cancellationToken);
        }

        private async Task<Quest> GenarateBaseQuest(long organizationId, IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organizations = await _organizationService.GetOrganizations(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            var organizationsForQuest = organizations
                .Where(o => o.Id != organizationId)
                .Where(o => lastQuests.All(q => q.Type != QuestType.BaseQuest || q.QuestEntity1Id != o.Id))
                .ToArray();

            var index = _random.Next(0, organizationsForQuest.Count() - 1);
            return CreateNewBaseQuest(organizationId, organizationsForQuest[index].Id);
        }

        private static Quest CreateNewBaseQuest(long organizationId, long questEntity1Id)
        {
            return new Quest
            {
                OrganizationId = organizationId,
                Created = DateTimeOffset.Now,
                Type = QuestType.BaseQuest,
                QuestEntity1Id = questEntity1Id,
                Status = QuestStatus.Created
            };
        }
    }
}
