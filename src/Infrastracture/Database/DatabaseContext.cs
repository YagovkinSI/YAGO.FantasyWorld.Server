using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Models;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<DataUpdate> DataUpdates { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<HistoryEvent> HistoryEvents { get; set; }
        public DbSet<HistoryEventEntityWeight> HistoryEventEntityWeights { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
               : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
