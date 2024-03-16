using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Quests;
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

        [HttpGet]
        [Route("getQuest")]
        public async Task<IEnumerable<string>> GetOrganizationRelations(long organizationFirstId, long organizationSecondId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _historyService.GetOrganizationRelations(organizationFirstId, organizationSecondId, cancellationToken);
        }
    }
}
