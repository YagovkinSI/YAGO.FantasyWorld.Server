using System;
using System.Threading.Tasks;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Результат решения квеста
    /// </summary>
    public class QuestOptionResult
    {
        public QuestOptionResult(string text, int weight, QuestOptionResultEntity[] questOptionResultEntities)
        {
            Text = text;
            Weight = weight;
            QuestOptionResultEntities = questOptionResultEntities;
        }

        /// <summary>
        /// Описание результа
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Вес результа
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Обработка результата
        /// </summary>
        public QuestOptionResultEntity[] QuestOptionResultEntities { get; }
    }
}
