using System;

namespace YAGO.FantasyWorld.Server.Domain
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
        public Quest Quest { get; set; }

        public QuestData(Quest quest) 
        {
            IsQuestReady = true;
            Quest = quest;
        }

        public QuestData(DateTimeOffset questReadyDateTime)
        {
            IsQuestReady = false;
            QuestReadyDateTime = questReadyDateTime;
        }
    }
}
