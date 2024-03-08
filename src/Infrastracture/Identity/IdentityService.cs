using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.ApiContracts.AuthorizationApi.Replies;
using YAGO.FantasyWorld.Domain.Exceptions;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Models;

namespace YAGO.FantasyWorld.Server.Infrastracture.Identity
{
    internal partial class IdentityService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserDatabaseService _userDatabaseService;
        private readonly ILogger<IdentityService> _logger;

        private readonly Dictionary<string, KnownError> KNOWN_IDENTITY_ERROR_CODES = new()
        {
            { "DuplicateUserName", new KnownError("Ошибка регистрации. Такой логин уже занят.", 409) },
            { "PasswordTooShort", new KnownError("Пароль должен содержать не менее 6 символов.", 400) },
            { "PasswordRequiresLower", new KnownError("Пароль должен содержать строчные латинские буквы 'a'-'z'.", 400) },
            { "PasswordRequiresUpper", new KnownError("Пароль должен содержать заглавные латинские буквы 'A'-'Z'.", 400) },
            { "PasswordRequiresDigit", new KnownError("Пароль должен содержать цифры '0'-'9'.", 400) },
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
            var newUser = new User
            {
                Email = string.Empty,
                UserName = userName,
                Registration = DateTimeOffset.Now,
                LastActivity = DateTimeOffset.Now
            };
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
                throw GetRegisterExeption(result.Errors);

            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userDatabaseService.FindByUserName(userName, cancellationToken);
            await _signInManager.PasswordSignInAsync(userName, password, true, false);

            return GetAuthorizationDataAsync(user);

        }

        public async Task<AuthorizationData> LoginAsync(string userName, string password, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            if (!result.Succeeded)
                throw new YagoException("Ошибка авторизации. Проверьте логин и пароль.");

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

        private static AuthorizationData GetAuthorizationDataAsync(YAGO.FantasyWorld.Domain.User user)
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
            var knownErrors = errors
                .Where(e => KNOWN_IDENTITY_ERROR_CODES.ContainsKey(e.Code))
                .Select(e => KNOWN_IDENTITY_ERROR_CODES[e.Code]);

            if (!knownErrors.Any())
            {
                _logger.LogError($"Ошибка регистрации. {string.Join(" .", errors.Select(e => $"{e.Code}: {e.Description}"))}");
                return new YagoException("Ошибка регистрации. Неизвестная ошибка.");
            }
            return new YagoException($"Ошибка регистрации. {string.Join(" ", knownErrors.Select(e => e.Message))}", knownErrors.Min(e => e.Code));
        }
    }
}
