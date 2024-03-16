using System;
using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Domain.Entities
{
    /// <summary>
    /// Игровая сущность
    /// </summary>
    public class YagoEntity
    {
        public YagoEntity(long id, EntityType entityType)
        {
            Id = id;
            EntityType = entityType;
        }

        public YagoEntity() { }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }
    }
}
