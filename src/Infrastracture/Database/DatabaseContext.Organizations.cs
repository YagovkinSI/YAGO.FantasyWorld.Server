using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IOrganizationDatabaseService
    {
        public Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedApplicationException();
        }

        public Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedApplicationException();
        }

        public Task SetUserForOrganization(long organizationId, string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new NotImplementedApplicationException();
        }
    }
}
