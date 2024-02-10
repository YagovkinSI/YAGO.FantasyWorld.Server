using System;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Application.Quests
{
    /// <summary>
    /// Сервис работы с квестами
    /// </summary>
    public class QuestService
    {
        private readonly IAuthorizationService _authorizationService;

        public QuestService(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Получение квеста
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<QuestData> GetQuest(System.Security.Claims.ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _authorizationService.GetCurrentUser(claimsPrincipal, cancellationToken);
            if (!user.IsAuthorized)
                throw new ApplicationException("Для получения квеста необходимо авторизоваться");
            return user.User.OrganizationId == null
                ? throw new ApplicationException("Для получения квеста необходимо выбрать организацию")
                : new QuestData
                {
                    IsQuestReady = false,
                    QuestReadyDateTime = DateTimeOffset.Now + TimeSpan.FromHours(1),
                };
        }

    }
}
