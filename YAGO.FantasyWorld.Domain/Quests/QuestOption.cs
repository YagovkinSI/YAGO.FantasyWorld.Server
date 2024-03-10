namespace YAGO.FantasyWorld.Domain.Quests
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

        public QuestOption() { }

        /// <summary>
        /// Идентификатор опции квеста
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание решения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Варианты результатов решения
        /// </summary>
        public QuestOptionResult[] QuestOptionResults { get; set; }
    }
}
