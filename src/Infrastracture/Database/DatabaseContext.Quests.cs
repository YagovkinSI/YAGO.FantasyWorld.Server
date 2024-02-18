using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain.Quests;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IQuestDatabaseService
    {
        public async Task<Quest> CreateQuest(Quest quest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var questDatabase = new Models.Quest
            {
                OrganizationId = quest.OrganizationId,
                Created = quest.Created,
                Type = quest.Type,
                QuestEntity1Id = quest.QuestEntity1Id,
                Status = quest.Status
            };
            Quests.Add(questDatabase);
            await SaveChangesAsync();
            return questDatabase.ToDomain();
        }

        public async Task<Quest> FindQuest(long questId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var quest = await Quests.FindAsync(new object[] { questId }, cancellationToken: cancellationToken);
            return quest.ToDomain();
        }

        public async Task<IEnumerable<Quest>> GetLastQuestes(long organizationId, int count, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var quests = await Quests
                .Where(q => q.OrganizationId == organizationId)
                .OrderByDescending(q => q.Created)
                .Take(count)
                .ToArrayAsync(cancellationToken: cancellationToken);
            return quests
                .Select(o => o.ToDomain());
        }
    }
}
