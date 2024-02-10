using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Quests;
using YAGO.FantasyWorld.Server.Domain;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestController : ControllerBase
    {
        private readonly QuestService _questService;

        public QuestController(QuestService questService)
        {
            _questService = questService;
        }

        [HttpGet]
        [Route("getQuest")]
        public async Task<QuestData> GetQuest(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _questService.GetQuest(HttpContext.User, cancellationToken);
        }
    }
}
