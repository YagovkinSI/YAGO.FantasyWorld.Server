using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain.HistoryEvents;

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
        /// <param name="historyEvent">Историческое событие</param>
        /// <param name="questId">Идентификатор квеста</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task HandleTransactionChange(HistoryEvent historyEvent, long questId, CancellationToken cancellationToken);
    }
}
