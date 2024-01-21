using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Сервис работы с данными организаций
    /// </summary>
    public interface IOrganizationDatabaseService
    {
        /// <summary>
        /// Получить список организаций
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список организаций</returns>
        Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken);

        /// <summary>
        /// Получить данные организации
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные организации</returns>
        Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken);

        /// <summary>
        /// Установить для организации пользователя как владельца
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="userId">Идентификатор пользователя</param>
        Task SetUserForOrganization(long organizationId, string userId, CancellationToken cancellationToken);
    }
}
