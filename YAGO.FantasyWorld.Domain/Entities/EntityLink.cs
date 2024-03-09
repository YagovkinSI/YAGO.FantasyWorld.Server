using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Domain.Entities
{
    /// <summary>
    /// Ссылка на сущность игры
    /// </summary>
    public class EntityLink<T>
    {
        public EntityLink(T id, EntityType entityType, string name)
        {
            Id = id;
            EntityType = entityType;
            Name = name;
        }

        public EntityLink() { }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string Name { get; set; }
    }
}
