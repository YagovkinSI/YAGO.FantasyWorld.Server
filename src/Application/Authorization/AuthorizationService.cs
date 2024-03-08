using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Application.UserLastActivity;
using YAGO.FantasyWorld.Domain.Exceptions;
using YAGO.FantasyWorld.ApiContracts.AuthorizationApi.Replies;

namespace YAGO.FantasyWorld.Server.Application.Authorization
{
    public class AuthorizationService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly UserLastActivityService _userLastActivityService;

        public AuthorizationService(
            IAuthorizationService authorizationService,
            UserLastActivityService userLastActivityService)
        {
            _authorizationService = authorizationService;
            _userLastActivityService = userLastActivityService;
        }

        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        public async Task<AuthorizationData> GetCurrentUser(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var authorizationData = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!authorizationData.IsAuthorized)
                return authorizationData;

            cancellationToken.ThrowIfCancellationRequested();
            await _userLastActivityService.UpdateUserLastActivity(authorizationData.User, cancellationToken);
            return authorizationData;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="passwordConfirm">Повторение пароля пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        public async Task<AuthorizationData> RegisterAsync(string userName, string password, string passwordConfirm, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateData(userName, password, passwordConfirm);

            cancellationToken.ThrowIfCancellationRequested();
            var authorizationData = await _authorizationService.RegisterAsync(userName, password, cancellationToken);
            if (!authorizationData.IsAuthorized)
                return authorizationData;

            cancellationToken.ThrowIfCancellationRequested();
            await _userLastActivityService.UpdateUserLastActivity(authorizationData.User, cancellationToken);
            return authorizationData;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        public async Task<AuthorizationData> LoginAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ValidateData(userName, password, password);

            cancellationToken.ThrowIfCancellationRequested();
            var authorizationData = await _authorizationService.LoginAsync(userName, password, cancellationToken);
            if (!authorizationData.IsAuthorized)
                return authorizationData;

            cancellationToken.ThrowIfCancellationRequested();
            await _userLastActivityService.UpdateUserLastActivity(authorizationData.User, cancellationToken);
            return authorizationData;
        }

        /// <summary>
        /// Выход пользователя из системы
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        public async Task LogoutAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var authorizationData = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!authorizationData.IsAuthorized)
                return;

            var tasks = new Task[2];
            tasks[0] = _userLastActivityService.UpdateUserLastActivity(authorizationData.User, cancellationToken);
            tasks[1] = _authorizationService.LogoutAsync(claimsPrincipal, cancellationToken);
            Task.WaitAll(tasks, cancellationToken);
        }

        private void ValidateData(string userName, string password, string passwordConfirm)
        {
            var errorList = new List<string>();

            if (string.IsNullOrEmpty(userName))
                errorList.Add("Необходимо указать логин.");
            if (string.IsNullOrEmpty(password))
                errorList.Add("Необходимо указать пароль.");
            if (!string.IsNullOrEmpty(password) && string.IsNullOrEmpty(passwordConfirm))
                errorList.Add("Необходимо повторить пароль.");
            if (password != passwordConfirm)
                errorList.Add("Введенные пароли не совпадают.");

            if (errorList.Any())
                throw new YagoException(string.Join(" ", errorList));
        }
    }
}
