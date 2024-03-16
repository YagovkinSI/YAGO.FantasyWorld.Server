using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Интерфейс получения данных сущности из БД
    /// </summary>
    public interface IEntityProviderDatabaseSerice
    {
        /// <summary>
        /// Получение названия/имени сущности
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <param name="entityId">Идентификатор сушности</param>
        /// <returns>Название/имя сущности</returns>
        Task<string> GetEntityName(EntityType entityType, long entityId);
    }
}
