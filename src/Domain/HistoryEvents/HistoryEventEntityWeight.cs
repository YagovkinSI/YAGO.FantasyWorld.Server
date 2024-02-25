using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.HistoryEvents
{
    /// <summary>
    /// Вес исторического события для сущности
    /// </summary>
    public class HistoryEventEntityWeight
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Идентификатор исторического события
        /// </summary>
        public long HistoryEventId { get; set; }

        /// <summary>
        /// Вес исторического события для сущности
        /// </summary>
        public int Weight { get; set; }
    }
}
