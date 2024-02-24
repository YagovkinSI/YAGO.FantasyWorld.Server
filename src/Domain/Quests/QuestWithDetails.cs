namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Данные квеста с деталаями
    /// </summary>
    public class QuestWithDetails
    {
        /// <summary>
        /// Данные квеста
        /// </summary>
        public Quest Quest { get; set; }

        /// <summary>
        /// Детали квеста
        /// </summary>
        public QuestDetails Details { get; set; }

        public QuestWithDetails(Quest quest, QuestDetails details)
        {
            Quest = quest;
            Details = details;
        }

    }
}
