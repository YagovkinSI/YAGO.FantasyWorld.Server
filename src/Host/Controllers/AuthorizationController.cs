using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Authorization;
using YAGO.FantasyWorld.Server.Application.Authorization.Models;
using YAGO.FantasyWorld.Server.Host.Models.Authorization;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationService _userAuthorizationService;

        public AuthorizationController(AuthorizationService userAuthorizationService)
        {
            _userAuthorizationService = userAuthorizationService;
        }

        [HttpGet]
        [Route("getCurrentUser")]
        public Task<AuthorizationData> GetCurrentUser(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _userAuthorizationService.GetCurrentUser(HttpContext.User, cancellationToken);
        }

        [HttpPost]
        [Route("register")]
        public Task<AuthorizationData> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!ModelState.IsValid)
            {
                var stateErrors = ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage));
                throw new BadHttpRequestException(string.Join(". ", stateErrors));
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _userAuthorizationService.RegisterAsync(request.UserName, request.Password, cancellationToken);
        }


        [HttpPost]
        [Route("login")]
        public Task<AuthorizationData> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!ModelState.IsValid)
            {
                var stateErrors = ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage));
                throw new BadHttpRequestException(string.Join(". ", stateErrors));
            }

            cancellationToken.ThrowIfCancellationRequested();
            return _userAuthorizationService.LoginAsync(request.UserName, request.Password, cancellationToken);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<AuthorizationData> Logout(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _userAuthorizationService.LogoutAsync(HttpContext.User, cancellationToken);
            return AuthorizationData.NotAuthorized;
        }
    }
}
