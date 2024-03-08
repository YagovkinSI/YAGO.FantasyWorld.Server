using System;
using System.Threading;
using System.Threading.Tasks;
using Yago.FantasyWorld.ApiContracts.Domain;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Application.UserLastActivity
{
    /// <summary>
    /// Сервис обновления даты и времени последней активности пользователя
    /// </summary>
    public class UserLastActivityService
    {
        private readonly IUserDatabaseService _userDatabaseService;

        public UserLastActivityService(IUserDatabaseService userDatabaseService)
        {
            _userDatabaseService = userDatabaseService;
        }

        /// <summary>
        /// Актуализация даты и времени последней активности пользователя
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task UpdateUserLastActivity(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null || user.LastActivity > DateTimeOffset.Now - TimeSpan.FromSeconds(5))
                return;

            await _userDatabaseService.UpdateLastActivity(user.Id, cancellationToken);
        }
    }
}
