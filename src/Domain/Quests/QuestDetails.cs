namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Детали квеста
    /// </summary>
    public class QuestDetails
    {
        /// <summary>
        /// Текст квеста
        /// </summary>
        public string QuestText { get; set; }

        /// <summary>
        /// Варианты решения
        /// </summary>
        public QuestOption[] QuestOptions { get; set; }

    }
}
