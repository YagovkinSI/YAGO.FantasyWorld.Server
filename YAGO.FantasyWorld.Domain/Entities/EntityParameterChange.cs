using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Domain.Entities
{
    /// <summary>
    /// Данные по изменению параметра сущности
    /// </summary>
    public class EntityParameterChange
    {
        public EntityParameterChange(EntityParameter entityParameter, string change)
        {
            EntityParameter = entityParameter;
            Change = change;
        }

        public EntityParameterChange() { }

        /// <summary>
        /// Параметр сущности
        /// </summary>
        public EntityParameter EntityParameter { get; set; }

        /// <summary>
        /// Изменение параметра
        /// </summary>
        public string Change { get; set; }
    }
}
