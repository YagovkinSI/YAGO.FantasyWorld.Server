namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Результат решения для параметра сущности
    /// </summary>
    public class QuestOptionResultEntityParameter
    {
        public QuestOptionResultEntityParameter(EntityParametres entityParameter, long change)
        {
            EntityParameter = entityParameter;
            Change = change;
        }

        /// <summary>
        /// Параметр сущности
        /// </summary>
        public EntityParametres EntityParameter { get; set; }

        /// <summary>
        /// Изменение параметра
        /// </summary>
        public long Change { get; set; }
    }
}
