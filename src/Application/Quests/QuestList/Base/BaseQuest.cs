using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Domain.Common;
using YAGO.FantasyWorld.Server.Domain.Enums;
using YAGO.FantasyWorld.Server.Domain.Quests;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.YagoException;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest : IQuestDetailsProvider
    {
        private readonly OrganizationService _organizationService;

        public BaseQuest(Quest quest, OrganizationService organizationService)
        {
            Quest = quest;
            _organizationService = organizationService;
        }

        public Quest Quest { get; }

        public QuestType Type => QuestType.BaseQuest;

        public async Task<QuestDetails> GetQuestForUser(Quest quest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organizationOpponent = await _organizationService.FindOrganization(quest.QuestEntity1Id, cancellationToken);

            return new QuestDetails
            {
                QuestText = $"Совет собрался, чтобы обсудить отношения с областью {organizationOpponent.Name}. " +
                    $"Некоторые члены совета предлагают сохранять нейтралитет и поддерживать мирные отношения. " +
                    $"Другие советуют искать взаимовыгодные сделки и укреплять экономическую связь. " +
                    $"Но есть и те, кто предлагает организовать набег с целью наживы и увеличения могущества.",
                QuestOptions = GetQuestOptions(quest)
            };
        }

        public Task<QuestOptionResult> HandleQuestOption(int questOptionId, CancellationToken cancellationToken)
        {
            var options = GetQuestOptions(Quest);
            var option = options.SingleOrDefault(o => o.Id == questOptionId);
            if (option == null)
                throw new ApplicationException("Неверный идентификатор варианта решения квеста.");

            var sumWeight = option.QuestOptionResults.Sum(r => r.Weight);
            var random = new Random().Next(1, sumWeight);

            QuestOptionResult result = null;
            var index = 0;
            while (random > 0)
            {
                result = option.QuestOptionResults[index];
                random -= result.Weight;
                index++;
            }

            return Task.FromResult(result);
        }

        private static QuestOption[] GetQuestOptions(Quest quest)
        {
            return new QuestOption[]
            {
                GetNeitralOption(quest),
                GetFriendlyOption(quest),
                //GetAgressiveOption(quest),
            };
        }

        private static EntityChange CreateChangeOrganization(long organizationId, int changeParameter)
        {
            return new EntityChange
            (
                EntityType.Organization,
                organizationId,
                new EntityParameterChange[]
                {
                    new(Domain.EntityParametres.OrganizationPower, changeParameter)
                }
            );
        }
    }
}
