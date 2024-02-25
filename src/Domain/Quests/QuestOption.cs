namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Вариант решения квеста
    /// </summary>
    public class QuestOption
    {
        public QuestOption(int id, string text, QuestOptionResult[] questOptionResults)
        {
            Id = id;
            Text = text;
            QuestOptionResults = questOptionResults;
        }

        /// <summary>
        /// Идентификатор опции квеста
        /// </summary>
        public int Id { get; }

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
