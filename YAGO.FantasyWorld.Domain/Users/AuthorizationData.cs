namespace YAGO.FantasyWorld.Domain.Users
{
    /// <summary>
    /// Данные авторизации
    /// </summary>
    public class AuthorizationData
    {
        /// <summary>
        /// Флаг, указывающий на наличие авторизации пользователя
        /// </summary>
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// Данные авторизованного пользователя (NULL если пользователь не авторизован)
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Создание данных неавторизованного пользователя
        /// </summary>
        public static AuthorizationData NotAuthorized => new() { IsAuthorized = false, User = null };
    }
}
