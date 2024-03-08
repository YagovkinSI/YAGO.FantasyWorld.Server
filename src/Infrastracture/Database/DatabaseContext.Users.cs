using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Users;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IUserDatabaseService
    {
        public async Task<User> Find(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await Users
                .Include(u => u.Organizations)
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);
            return user?.ToDomain();
        }

        public async Task<User> FindByUserName(string userName, CancellationToken cancellationToken)
        {
            var user = await Users
                .Include(u => u.Organizations)
                .SingleOrDefaultAsync(u => u.UserName == userName, cancellationToken: cancellationToken);
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
            SaveChanges();
        }
    }
}
