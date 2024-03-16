using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using YAGO.FantasyWorld.Domain.HistoryEvents.Enums;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    /// <summary>
    /// Историческое событие
    /// </summary>
    public class HistoryEvent
    {
        /// <summary>
        /// Идентификтаор исторического события
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата и время события (реальное)
        /// </summary>
        public DateTimeOffset DateTimeUtc { get; set; }

        /// <summary>
        /// Тип события
        /// </summary>
        public HistoryEventType Type { get; set; }

        /// <summary>
        /// Участники исторического события
        /// </summary>
        public string HistoryEventEntities { get; set; }

        /// <summary>
        /// Изменения параметров
        /// </summary>
        public string ParameterChanges { get; set; }

        /// <summary>
        /// Список веса события для сущностей
        /// </summary>
        public virtual List<HistoryEventEntityWeight> EntityWeights { get; set; }

        internal static void CreateModel(ModelBuilder builder)
        {
            var model = builder.Entity<HistoryEvent>();
            model.HasKey(m => m.Id);

            model.HasIndex(m => m.DateTimeUtc);
            model.HasIndex(m => m.Type);
        }

        internal Domain.HistoryEvents.HistoryEvent ToDomain()
        {
            return new Domain.HistoryEvents.HistoryEvent
            {
                Id = Id,
                Type = Type,
                DateTimeUtc = DateTimeUtc,
                HistoryEventEntities = JsonConvert.DeserializeObject<Domain.HistoryEvents.HistoryEventEntity[]>(HistoryEventEntities),
                ParameterChanges = JsonConvert.DeserializeObject<Domain.Entities.EntityChange[]>(ParameterChanges),
                EntityWeights = EntityWeights.Select(w => w.ToDomain()).ToArray()
            };
        }
    }
}
