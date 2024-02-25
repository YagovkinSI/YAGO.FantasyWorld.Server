using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Провайдер деталей квеста
    /// </summary>
    public interface IQuestDetailsProvider
    {
        /// <summary>
        /// Данные квеста
        /// </summary>
        Quest Quest { get; }

        /// <summary>
        /// Тип квеста
        /// </summary>
        QuestType Type { get; }

        /// <summary>
        /// Получение деталей квеста
        /// </summary>
        /// <param name="quest">Квест</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Квест для пользователя</returns>
        Task<QuestDetails> GetQuestForUser(Quest quest, CancellationToken cancellationToken);

        /// <summary>
        /// Обработать выбор решения квеста
        /// </summary>
        /// <param name="questOptionId">Идентификатор выбранного решения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Результат квеста</returns>
        Task<QuestOptionResult> HandleQuestOption(int questOptionId, CancellationToken cancellationToken);
    }
}
