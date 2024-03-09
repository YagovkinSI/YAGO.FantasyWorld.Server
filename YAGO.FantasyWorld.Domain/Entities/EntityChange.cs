using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Domain.Entities
{
    /// <summary>
    /// Данные по изменению параметров сущности
    /// </summary>
    public class EntityChange
    {
        public EntityChange(EntityType entityType,
            long entityId,
            EntityParameterChange[] entityParametersChange)
        {
            EntityType = entityType;
            EntityId = entityId;
            EntityParametersChange = entityParametersChange;
        }

        public EntityChange() { }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Изменения параметров
        /// </summary>
        public EntityParameterChange[] EntityParametersChange { get; set; }
    }
}
