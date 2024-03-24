using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Server.Application.History;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryEventController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryEventController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        [Route("getHistoryEvents")]
        public async Task<IEnumerable<string>> GetHistoryEvents(HistoryEventFilter historyEventFilter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _historyService.GetHistoryEvents(historyEventFilter, cancellationToken);
        }
    }
}
