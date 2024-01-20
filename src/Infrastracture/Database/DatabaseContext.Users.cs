using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IUserDatabaseService
    {
        public async Task<Domain.User> Find(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await Users.FindAsync(new object[] { userId }, cancellationToken: cancellationToken);
            return user?.ToDomain();
        }

        public async Task<Domain.User> FindByUserName(string userName, CancellationToken cancellationToken)
        {
            var user = await Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken: cancellationToken);
            return user?.ToDomain();
        }

        public async Task UpdateLastActivity(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await Users.FindAsync(new object[] { userId }, cancellationToken: cancellationToken);
            if (user == null)
                return;

            user.LastActivity = DateTimeOffset.Now;
            Update(user);
            await SaveChangesAsync();
        }
    }
}
