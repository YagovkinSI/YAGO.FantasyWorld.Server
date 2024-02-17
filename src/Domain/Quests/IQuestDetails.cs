using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain.Quests
{
    /// <summary>
    /// Детали и обработка типа квеста
    /// </summary>
    public interface IQuestDetails
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
        /// Получение квеста для пользователя
        /// </summary>
        /// <param name="quest">Квест</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Квест для пользователя</returns>
        Task<QuestForUser> GetQuestForUser(Quest quest, CancellationToken cancellationToken);

        /// <summary>
        /// Обработать выбор решения квеста
        /// </summary>
        /// <param name="questOptionId">Идентификатор выбранного решения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Результат квеста</returns>
        Task<string> HandleQuestOption(int questOptionId, CancellationToken cancellationToken);
    }
}
