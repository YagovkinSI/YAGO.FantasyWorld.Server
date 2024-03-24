using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.HistoryEvents;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Интерфейс получения исторических событий из БД
    /// </summary>
    public interface IHistoryEventDatabaseSerice
    {
        /// <summary>
        /// Получение истории сущностей
        /// </summary>
        /// <param name="historyEventFilter">Фильтр получения исторических событий</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список событий</returns>
        Task<HistoryEvent[]> GetHistoryEvents(HistoryEventFilter historyEventFilter, CancellationToken cancellationToken);
    }
}
