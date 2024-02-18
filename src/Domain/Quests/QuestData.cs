﻿using System;

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
        public QuestForUser Quest { get; set; }

        public QuestData(QuestForUser quest)
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