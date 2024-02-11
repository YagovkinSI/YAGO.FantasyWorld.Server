namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Вариант решения квеста
    /// </summary>
    public class QuestOption
    {
        public QuestOption(string text, QuestOptionResult[] questOptionResults)
        {
            Text = text;
            QuestOptionResults = questOptionResults;
        }

        /// <summary>
        /// Описание решения
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Варианты результатов решения
        /// </summary>
        public QuestOptionResult[] QuestOptionResults { get; }
    }
}
