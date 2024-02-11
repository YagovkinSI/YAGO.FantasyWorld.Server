using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Детали и обработка типа квеста
    /// </summary>
    public interface IQuestDetails
    {
        /// <summary>
        /// Тип квеста
        /// </summary>
        QuestType Type { get; }

        /// <summary>
        /// Варианты решения квеста
        /// </summary>
        QuestOption[] Options { get; }

        /// <summary>
        /// Количество вариантов решения
        /// </summary>
        int CountOptions { get; }

        /// <summary>
        /// Получение квеста для пользователя
        /// </summary>
        /// <param name="quest">Квест</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Квест для пользователя</returns>
        Task<QuestForUser> GetQuestForUser(Quest quest, CancellationToken cancellationToken);

        /// <summary>
        /// Обработать выбор решения квеста
        /// </summary>
        /// <param name="questOptionIndex">индекс выбранного решения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Результат квеста</returns>
        Task<string> HandleQuestOption(int questOptionIndex, CancellationToken cancellationToken);
    }
}
