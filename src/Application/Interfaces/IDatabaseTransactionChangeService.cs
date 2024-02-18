using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.Quests;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Обработка запросов на изменение нескольких полей
    /// </summary>
    public interface IDatabaseTransactionChangeService
    {
        /// <summary>
        /// Обработка запросов на изменение нескольких полей
        /// </summary>
        /// <param name="questOptionResultEntities">Список изменений</param>
        /// <param name="questId">Идентификатор квеста</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task HandleTransactionChange(QuestOptionResultEntity[] questOptionResultEntities, long questId, CancellationToken cancellationToken);
    }
}
