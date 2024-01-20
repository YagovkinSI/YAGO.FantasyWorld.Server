using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IOrganizationDatabaseService
    {
        public async Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organizations = await Organizations.ToArrayAsync(cancellationToken: cancellationToken);
            return organizations
                .Select(o => o.ToDomain());
        }

        public async Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organization = await Organizations.FindAsync(new object[] { organizationId }, cancellationToken: cancellationToken);
            return organization?.ToDomain();
        }

        public async Task SetUserForOrganization(long organizationId, string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organization = await Organizations.FindAsync(new object[] { organizationId }, cancellationToken: cancellationToken);
            if (organization == null)
                throw new ApplicationException(string.Format("Организация с ID={0} не найдена.", organizationId), 400);

            organization.UserId = userId;
            Update(organization);
            SaveChanges();
        }
    }
}
