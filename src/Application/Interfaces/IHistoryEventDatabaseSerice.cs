using System.Collections.Generic;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities;
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
        /// <param name="entityObjects">Объекты участники</param>
        /// <param name="eventCount">Колличиество записей</param>
        /// <returns>Список событий</returns>
        Task<HistoryEvent[]> GetOrganizationHistory(IEnumerable<YagoEntity> entityObjects, int eventCount);
    }
}
