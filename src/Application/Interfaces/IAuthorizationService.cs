using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.ApiContracts.AuthorizationApi.Replies;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Сервис авторизации пользователей
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        Task<AuthorizationData> GetCurrentUser(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        Task<AuthorizationData> RegisterAsync(string userName, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        Task<AuthorizationData> LoginAsync(string userName, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Выход пользователя из системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        Task LogoutAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken);
    }
}
