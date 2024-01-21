using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Application.Admin
{
    /// <summary>
    /// Сервис администрирования
    /// </summary>
    public class AdminService
    {
        private readonly IFillDatabaseService _fillDatabaseService;

        public AdminService(IFillDatabaseService fillDatabaseService)
        {
            _fillDatabaseService = fillDatabaseService;
        }

        /// <summary>
        /// Наполнить данными базу данных
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        public async Task FillDatabase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _fillDatabaseService.FillDatabase(cancellationToken);
        }
    }
}
