using YAGO.FantasyWorld.Server.Domain.Common;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization
    {
        public Organization(long id, string name, string description, int power, IdLink<string> userLink)
        {
            Id = id;
            Name = name;
            Description = description;
            Power = power;
            UserLink = userLink;
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
        /// Ссылка на пользователя
        /// </summary>
        public IdLink<string> UserLink { get; set; }
    }
}
