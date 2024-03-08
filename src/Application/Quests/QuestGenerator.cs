using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Quests;
using YAGO.FantasyWorld.Domain.Quests.Enums;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Application.Organizations;

namespace YAGO.FantasyWorld.Server.Application.Quests
{
    public class QuestGenerator
    {
        private readonly IQuestDatabaseService _questDatabaseService;
        private readonly OrganizationService _organizationService;

        private readonly Random _random = new();

        public QuestGenerator(IQuestDatabaseService questDatabaseService,
            OrganizationService organizationService)
        {
            _questDatabaseService = questDatabaseService;
            _organizationService = organizationService;
        }

        internal async Task<Quest> GenerateQuest(long organizationId, IEnumerable<Quest> lastQuests, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var newQuest = await GenerateNewQuest(organizationId, lastQuests, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            return await _questDatabaseService.CreateQuest(newQuest, cancellationToken);
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
