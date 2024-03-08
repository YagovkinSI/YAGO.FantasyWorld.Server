using System;
using YAGO.FantasyWorld.Server.Domain.Common;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.HistoryEvents
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
        public HistoryEventEntity[] HistoryEventEntities { get; set; }

        /// <summary>
        /// Вес исторического события для сущностей
        /// </summary>
        public HistoryEventEntityWeight[] EntityWeights { get; set; }

        /// <summary>
        /// Изменения параметров
        /// </summary>
        public EntityChange[] ParameterChanges { get; set; }
    }
}
