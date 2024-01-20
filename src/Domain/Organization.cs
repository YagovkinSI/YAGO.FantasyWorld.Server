namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public long Name { get; set; }

        /// <summary>
        /// Описание организации
        /// </summary>
        public long Description { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
    }
}
