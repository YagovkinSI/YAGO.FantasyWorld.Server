using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Models;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Organization> Organizations { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
               : base(options)
        {
            Database.Migrate();
        }
    }
}
