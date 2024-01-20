using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Domain;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Сервис работы с данными пользователя
    /// </summary>
    public interface IUserDatabaseService
    {
        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные пользователя</returns>
        Task<User> Find(string userId, CancellationToken cancellationToken);

        /// <summary>
        /// Получение данных пользователя по имени пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя (логин)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Данные пользователя</returns>
        Task<User> FindByUserName(string userName, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление времени последней активности
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task UpdateLastActivity(string userId, CancellationToken cancellationToken);
    }
}
