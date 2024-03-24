using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IHistoryEventDatabaseSerice
    {
        public async Task<HistoryEvent[]> GetHistoryEvents(HistoryEventFilter historyEventFilter, CancellationToken cancellationToken)
        {
            var historyEvents = HistoryEvents
                .Include(e => e.EntityWeights)
                .Where(e => e.DateTimeUtc > historyEventFilter.DateTimeUtcMin);

            foreach (var entityObject in historyEventFilter.Entities)
            {
                historyEvents = historyEvents
                    .Where(e => e.EntityWeights.Any(w => w.EntityType == entityObject.EntityType && w.EntityId == entityObject.Id));
            }

            var dateTimeUtcNow = DateTimeOffset.UtcNow;
            historyEvents = historyEvents
                .OrderByDescending(e => e.EntityWeights.Sum(w => w.Weight) - EF.Functions.DateDiffDay(e.DateTimeUtc, dateTimeUtcNow))
                .Skip((historyEventFilter.PageNum - 1) * historyEventFilter.EventCount)
                .Take(historyEventFilter.EventCount)
                .OrderByDescending(e => e.DateTimeUtc);

            return await historyEvents
                .Select(e => e.ToDomain())
                .ToArrayAsync();
        }
    }
}
