using Microsoft.EntityFrameworkCore;
using System;
using YAGO.FantasyWorld.Domain.Quests.Enums;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    public class Quest
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public DateTimeOffset Created { get; set; }
        public QuestType Type { get; set; }
        public long QuestEntity1Id { get; set; }
        public QuestStatus Status { get; set; }

        public virtual Organization Organization { get; set; }

        internal static void CreateModel(ModelBuilder builder)
        {
            var model = builder.Entity<Quest>();
            model.HasKey(m => m.Id);
            model.HasOne(m => m.Organization)
                .WithMany(m => m.Quests)
                .HasForeignKey(m => m.OrganizationId);

            model.HasIndex(m => m.Created);
            model.HasIndex(m => m.Status);
        }

        internal Domain.Quests.Quest ToDomain()
        {
            return new Domain.Quests.Quest
            {
                Id = Id,
                OrganizationId = OrganizationId,
                Created = Created,
                Type = Type,
                QuestEntity1Id = QuestEntity1Id,
                Status = Status
            };
        }
    }
}
