﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Organizations;
using YAGO.FantasyWorld.Domain.Users;
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

        [HttpPost]
        [Route("setCurrentUserForOrganization")]
        public async Task<AuthorizationData> SetCurrentUserForOrganization(SetOrganizationUserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationService.SetCurrentUserForOrganization(request.OrganizationId, HttpContext.User, cancellationToken);
        }
    }
}
