using System.Threading;
using System.Threading.Tasks;
using Yago.FantasyWorld.ApiContracts.Domain;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Application.Users
{
    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    public class UserService
    {
        private readonly IUserDatabaseService _userDatabaseService;

        public UserService(IUserDatabaseService userDatabaseService)
        {
            _userDatabaseService = userDatabaseService;
        }

        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> FindUser(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _userDatabaseService.Find(userId, cancellationToken);
        }
    }
}
