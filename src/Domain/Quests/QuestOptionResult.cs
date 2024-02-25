using System;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Результат решения квеста
    /// </summary>
    public class QuestOptionResult
    {
        public QuestOptionResult(QuestOptionResultType type, string text, int weight, QuestOptionResultEntity[] questOptionResultEntities)
        {
            Type = type;
            Text = text;
            Weight = weight;
            QuestOptionResultEntities = questOptionResultEntities;
        }

        /// <summary>
        /// Тип результата
        /// </summary>
        public QuestOptionResultType Type { get; }

        /// <summary>
        /// Описание результа
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Вес результа
        /// </summary>
        public int Weight { get; }

        /// <summary>
        /// Изменения параметров сущностей по результатам
        /// </summary>
        public QuestOptionResultEntity[] QuestOptionResultEntities { get; }
    }
}
