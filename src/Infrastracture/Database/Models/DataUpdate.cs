using Microsoft.EntityFrameworkCore;
using System;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    public class DataUpdate
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset UpdateDateTime { get; set; }

        internal static void CreateModel(ModelBuilder builder)
        {
            var model = builder.Entity<DataUpdate>();
            model.HasKey(m => m.Id);

            model.HasIndex(m => m.Version);
        }
    }
}
