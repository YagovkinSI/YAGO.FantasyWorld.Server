using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Application.Organizations
{
    /// <summary>
    /// Сервис работы с организациями
    /// </summary>
    public class OrganizationService
    {
        /// <summary>
        /// Получить список организаций
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список организаций</returns>
        public async Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken) => throw new NotImplementedApplicationException();

        /// <summary>
        /// Получить данные организации
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные организации</returns>
        public async Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken) => throw new NotImplementedApplicationException();

        /// <summary>
        /// Установить для организации пользователя как владельца
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public async Task SetUserForOrganization(long organizationId, string userId, CancellationToken cancellationToken) => throw new NotImplementedApplicationException();
    }
}
