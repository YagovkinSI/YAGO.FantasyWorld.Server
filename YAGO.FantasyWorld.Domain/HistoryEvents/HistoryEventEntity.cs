using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.HistoryEvents.Enums;

namespace YAGO.FantasyWorld.Domain.HistoryEvents
{
    /// <summary>
    /// Результат решения для одной сущности
    /// </summary>
    public class HistoryEventEntity
    {
        public HistoryEventEntity(EntityType entityType,
            long entityId,
            HitsoryEventEnitiyRole role)
        {
            EntityType = entityType;
            EntityId = entityId;
            Role = role;
        }

        public HistoryEventEntity() { }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Роль сущности в историческом событии
        /// </summary>
        public HitsoryEventEnitiyRole Role { get; set; }
    }
}
