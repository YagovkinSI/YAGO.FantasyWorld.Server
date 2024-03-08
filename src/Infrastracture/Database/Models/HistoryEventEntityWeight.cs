using Microsoft.EntityFrameworkCore;
using Yago.FantasyWorld.ApiContracts.Common.Enums;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database.Models
{
    /// <summary>
    /// Вес исторического события для сущности
    /// </summary>
    public class HistoryEventEntityWeight
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Идентификатор исторического события
        /// </summary>
        public long HistoryEventId { get; set; }

        /// <summary>
        /// Вес исторического события для сущности
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Историческое событие
        /// </summary>
        public virtual HistoryEvent HistoryEvent { get; set; }

        internal static void CreateModel(ModelBuilder builder)
        {
            var model = builder.Entity<HistoryEventEntityWeight>();
            model.HasKey(m => m.Id);

            model.HasOne(m => m.HistoryEvent)
                .WithMany(m => m.EntityWeights)
                .HasForeignKey(m => m.HistoryEventId);

            model.HasIndex(m => m.HistoryEventId);
            model.HasIndex(m => new { m.EntityType, m.EntityId });
            model.HasIndex(m => m.Weight);
        }
    }
}
