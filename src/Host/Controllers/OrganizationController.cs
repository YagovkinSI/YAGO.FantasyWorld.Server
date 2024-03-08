using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Yago.FantasyWorld.ApiContracts.Domain;
using YAGO.FantasyWorld.Server.Application.Authorization.Models;
using YAGO.FantasyWorld.Server.Application.Organizations;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : Controller
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
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
        public async Task<AuthorizationData> SetCurrentUserForOrganization(long organizationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationService.SetCurrentUserForOrganization(organizationId, HttpContext.User, cancellationToken);
        }
    }
}
