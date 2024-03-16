using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IHistoryEventDatabaseSerice
    {
        public async Task<HistoryEvent[]> GetOrganizationHistory(IEnumerable<YagoEntity> entityObjects, int eventCount)
        {
            var historyEvents = HistoryEvents
                .Include(e => e.EntityWeights)
                .Where(e => e.EntityWeights.Any());

            foreach (var entityObject in entityObjects)
            {
                historyEvents = historyEvents
                    .Where(e => e.EntityWeights.Any(w => w.EntityType == entityObject.EntityType && w.EntityId == entityObject.Id));
            }

            historyEvents = historyEvents
                .OrderByDescending(e => e.DateTimeUtc)
                .Take(3);

            return await historyEvents
                .Select(e => e.ToDomain())
                .ToArrayAsync();
        }
    }
}
