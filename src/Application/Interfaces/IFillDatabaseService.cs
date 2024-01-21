using System.Threading;
using System.Threading.Tasks;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    /// <summary>
    /// Сервис наполнения данными базы данных
    /// </summary>
    public interface IFillDatabaseService
    {
        /// <summary>
        /// Наполнить данными базу данных
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        Task FillDatabase(CancellationToken cancellationToken);
    }
}
