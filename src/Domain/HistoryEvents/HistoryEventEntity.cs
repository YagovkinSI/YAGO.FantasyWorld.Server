using Yago.FantasyWorld.ApiContracts.Common.Enums;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.HistoryEvents
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
