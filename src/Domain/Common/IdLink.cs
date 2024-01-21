namespace YAGO.FantasyWorld.Server.Domain.Common
{
    /// <summary>
    /// Ссылка на сущность игры
    /// </summary>
    public class IdLink<T>
    {
        public IdLink(T id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string Name { get; set; }
    }
}
