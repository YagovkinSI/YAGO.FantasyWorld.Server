using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Результат решения для одной сущности
    /// </summary>
    public class QuestOptionResultEntity
    {
        public QuestOptionResultEntity(EntityType entityType,
            long entityId,
            QuestOptionResultEntityParameter[] questOptionResultEntityParameters)
        {
            EntityType = entityType;
            EntityId = entityId;
            QuestOptionResultEntityParameters = questOptionResultEntityParameters;
        }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Изменнеия параметров
        /// </summary>
        public QuestOptionResultEntityParameter[] QuestOptionResultEntityParameters { get; set; }
    }
}
