using System.ComponentModel.DataAnnotations;

namespace YAGO.FantasyWorld.Server.Host.Models.Authorization
{
    /// <summary>
    /// Запрос авторизации
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
