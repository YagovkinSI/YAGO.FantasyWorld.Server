using System;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Данные квеста
    /// </summary>
    public class QuestData
    {
        /// <summary>
        /// Флаг готовности квеста
        /// </summary>
        public bool IsQuestReady { get; set; }

        /// <summary>
        /// Время готовности квеста
        /// </summary>
        public DateTimeOffset? QuestReadyDateTime { get; set; }

        /// <summary>
        /// Данные квеста
        /// </summary>
        public QuestWithDetails QuestWithDetails { get; set; }

        public QuestData(QuestWithDetails quest)
        {
            IsQuestReady = true;
            QuestWithDetails = quest;
        }

        public QuestData(DateTimeOffset questReadyDateTime)
        {
            IsQuestReady = false;
            QuestReadyDateTime = questReadyDateTime;
        }
    }
}
