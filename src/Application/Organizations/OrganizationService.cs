using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Authorization.Models;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Exceptions;

namespace YAGO.FantasyWorld.Server.Application.Organizations
{
    /// <summary>
    /// Сервис работы с организациями
    /// </summary>
    public class OrganizationService
    {
        private readonly IOrganizationDatabaseService _organizationDatabaseService;
        private readonly IAuthorizationService _authorizationService;

        public OrganizationService(IOrganizationDatabaseService organizationDatabaseService, IAuthorizationService authorizationService)
        {
            _organizationDatabaseService = organizationDatabaseService;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Получить список организаций
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список организаций</returns>
        public async Task<IEnumerable<Organization>> GetOrganizations(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationDatabaseService.GetOrganizations(cancellationToken);
        }

        /// <summary>
        /// Получить данные организации
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные организации</returns>
        public async Task<Organization> FindOrganization(long organizationId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _organizationDatabaseService.FindOrganization(organizationId, cancellationToken);
        }

        /// <summary>
        /// Установить для организации текущего пользователя как владельца
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        /// <param name="claimsPrincipal">Ифнормация о пользователе запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные авторизации</returns>
        public async Task<AuthorizationData> SetCurrentUserForOrganization(long organizationId, ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var authorizationData = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!authorizationData.IsAuthorized)
                throw new NotAuthorizedApplicationException();

            if (authorizationData.User.OrganizationId != null)
                throw new ApplicationException("У вас уже есть организация.", 400);

            cancellationToken.ThrowIfCancellationRequested();
            var organization = await FindOrganization(organizationId, cancellationToken);
            if (organization == null)
                throw new ApplicationException(string.Format("Организация с ID={0} не найдена.", organizationId), 400);

            if (organization.UserLink != null)
                throw new ApplicationException(string.Format("Организация с ID={0} уже занята другим игроком.", organizationId), 400);

            cancellationToken.ThrowIfCancellationRequested();
            await _organizationDatabaseService.SetUserForOrganization(organizationId, authorizationData.User.Id, cancellationToken);

            authorizationData.User.OrganizationId = organizationId;
            return authorizationData;
        }
    }
}
