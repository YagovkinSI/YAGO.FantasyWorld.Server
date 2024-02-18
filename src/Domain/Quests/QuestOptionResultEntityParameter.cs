﻿namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Результат решения для параметра сущности
    /// </summary>
    public class QuestOptionResultEntityParameter
    {
        public QuestOptionResultEntityParameter(EntityParametres entityParameter, double change)
        {
            EntityParameter = entityParameter;
            Change = change.ToString();
        }

        public QuestOptionResultEntityParameter(EntityParametres entityParameter, string change)
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
        public string Change { get; set; }
    }
}