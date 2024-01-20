using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : Controller
    {
        private readonly OrganizationService _organizationService;
        private readonly IAuthorizationService _authorizationService;

        public OrganizationController(OrganizationService organizationService, IAuthorizationService authorizationService)
        {
            _organizationService = organizationService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Route("getOrganizations")]
        public async Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationService.GetOrganizations(cancellationToken);
        }

        [HttpGet]
        [Route("findOrganization")]
        public async Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationService.FindOrganization(organizationId, cancellationToken);
        }

        [HttpGet]
        [Route("setCurrentUserForOrganization")]
        public async Task SetCurrentUserForOrganization(long organizationId, CancellationToken cancellationToken)
        {
            var authorizationData = await _authorizationService.GetCurrentUser(HttpContext.User, cancellationToken);
            if (!authorizationData.IsAuthorized)
                throw new NotAuthorizedApplicationException();

            cancellationToken.ThrowIfCancellationRequested();
            await _organizationService.SetUserForOrganization(organizationId, authorizationData.User.Id, cancellationToken);
        }
    }
}
