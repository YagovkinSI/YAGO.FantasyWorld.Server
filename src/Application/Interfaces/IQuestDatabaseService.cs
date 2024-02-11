using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Сервис работы с данными пользователя
    /// </summary>
    public interface IQuestDatabaseService
    {
        /// <summary>
        /// Получение данных по последним квестам
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="count">Количество последних квестов</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные последних квестов</returns>
        Task<IEnumerable<Quest>> GetLastQuestes(long organizationId, int count, CancellationToken cancellationToken);

        /// <summary>
        /// Создание нового квеста
        /// </summary>
        /// <param name="quest">Данные создаваемого квеста</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный квест</returns>
        Task<Quest> CreateQuest(Quest quest, CancellationToken cancellationToken);

        /// <summary>
        /// Поиск квеста по Id
        /// </summary>
        /// <param name="questId">Идентификатор квеста</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные квеста</returns>
        Task<Quest> FindQuest(long questId, CancellationToken cancellationToken);
    }
}
