using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Authorization.Models;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Models;

namespace YAGO.FantasyWorld.Server.Infrastracture.Identity
{
    internal class IdentityService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserDatabaseService _userDatabaseService;
        private readonly ILogger<IdentityService> _logger;

        private readonly Dictionary<string, Domain.Exceptions.ApplicationException> KNOWN_IDENTITY_ERROR_CODES = new()
        {
            { "DuplicateUserName", new Domain.Exceptions.ApplicationException("Ошибка регистрации. Такой логин уже занят.", 409) }
        };

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserDatabaseService userDatabaseService,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userDatabaseService = userDatabaseService;
            _logger = logger;
        }

        public async Task<AuthorizationData> GetCurrentUser(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            return ToAuthorizationData(user);
        }

        public async Task<AuthorizationData> RegisterAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = new User
            {
                Email = string.Empty,
                UserName = userName,
                Registration = DateTimeOffset.Now,
                LastActivity = DateTimeOffset.Now
            };
            var result = await _userManager.CreateAsync(user, password);

            cancellationToken.ThrowIfCancellationRequested();
            if (!result.Succeeded)
                throw GetRegisterExeption(result.Errors);
            await _signInManager.SignInAsync(user, true);
            return ToAuthorizationData(user);
        }

        public async Task<AuthorizationData> LoginAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            if (!result.Succeeded)
                throw new Domain.Exceptions.ApplicationException("Ошибка авторизации. Проверьте логин и пароль.");

            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userDatabaseService.FindByUserName(userName, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            return GetAuthorizationDataAsync(user);
        }

        public async Task LogoutAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                return;

            cancellationToken.ThrowIfCancellationRequested();
            await _signInManager.SignOutAsync();
        }

        private static AuthorizationData ToAuthorizationData(User user)
        {
            if (user == null)
                return AuthorizationData.NotAuthorized;

            var domainUser = user.ToDomain();
            return GetAuthorizationDataAsync(domainUser);
        }

        private static AuthorizationData GetAuthorizationDataAsync(Domain.User user)
        {
            return user == null
                ? AuthorizationData.NotAuthorized
                : new AuthorizationData
                {
                    IsAuthorized = true,
                    User = user
                };
        }

        private Exception GetRegisterExeption(IEnumerable<IdentityError> errors)
        {
            var error = errors.FirstOrDefault(e => KNOWN_IDENTITY_ERROR_CODES.ContainsKey(e.Code));
            if (error == null)
            {
                _logger.LogError($"Ошибка регистрации. {string.Join(" .", errors.Select(e => $"{e.Code}: {e.Description}"))}");
                return new Domain.Exceptions.ApplicationException("Ошибка регистрации. Неизвестная ошибка.");
            }
            return KNOWN_IDENTITY_ERROR_CODES[error.Code];
        }
    }
}
