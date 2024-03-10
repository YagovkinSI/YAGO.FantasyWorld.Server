using YAGO.FantasyWorld.Domain.Entities;

namespace YAGO.FantasyWorld.Domain.Organizations
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization
    {
        public Organization(long id, string name, string description, int power, EntityLink<string> userLink)
        {
            Id = id;
            Name = name;
            Description = description;
            Power = power;
            UserLink = userLink;
        }

        public Organization() { }

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
        /// Ссылка на пользователя
        /// </summary>
        public EntityLink<string> UserLink { get; set; }
    }
}
