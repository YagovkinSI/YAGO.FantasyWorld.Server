using Microsoft.EntityFrameworkCore;
using YAGO.FantasyWorld.Server.Domain.Common;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    public class Organization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Power { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }

        internal static void CreateModel(ModelBuilder builder)
        {
            var model = builder.Entity<Organization>();
            model.HasKey(m => m.Id);
            model.HasOne(m => m.User)
                .WithMany(m => m.Organizations)
                .HasForeignKey(m => m.UserId);

            model.HasIndex(m => m.UserId);
        }

        public Domain.Organization ToDomain()
        {
            var userLink = UserId == null
                ? null
                : new IdLink<string>(User.Id, User.UserName);

            return new Domain.Organization
            (
                Id,
                Name,
                Description,
                Power,
                userLink
            );
        }
    }
}
