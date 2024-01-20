namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization
    {
        public Organization(long id, string name, string description, int power, string userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Power = power;
            UserId = userId;
        }

        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание организации
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Могущество организации
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
    }
}
