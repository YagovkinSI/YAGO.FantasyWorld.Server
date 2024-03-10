using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Quests.Enums;

namespace YAGO.FantasyWorld.Domain.Quests
{
    /// <summary>
    /// Результат решения квеста
    /// </summary>
    public class QuestOptionResult
    {
        public QuestOptionResult(QuestOptionResultType type, string text, int weight, EntityChange[] entitiesChange)
        {
            Type = type;
            Text = text;
            Weight = weight;
            EntitiesChange = entitiesChange;
        }

        public QuestOptionResult() { }

        /// <summary>
        /// Тип результата
        /// </summary>
        public QuestOptionResultType Type { get; set; }

        /// <summary>
        /// Описание результа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Вес результа
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Изменения параметров сущностей по результатам
        /// </summary>
        public EntityChange[] EntitiesChange { get; set; }
    }
}
