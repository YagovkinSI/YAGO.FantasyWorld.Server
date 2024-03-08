using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.Common
{
    /// <summary>
    /// Данные по изменению параметров сущности
    /// </summary>
    public class EntityChange
    {
        public EntityChange(EntityType entityType,
            long entityId,
            EntityParameterChange[] questOptionResultEntityParameters)
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
        public EntityParameterChange[] QuestOptionResultEntityParameters { get; set; }
    }
}
