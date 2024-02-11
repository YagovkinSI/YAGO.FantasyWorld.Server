using System;
using System.Threading.Tasks;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Результат решения квеста
    /// </summary>
    public class QuestOptionResult
    {
        public QuestOptionResult(string text, int weight, Func<Task<string>> handle)
        {
            Text = text;
            Weight = weight;
            Handle = handle;
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
        public Func<Task<string>> Handle { get; }
    }
}
