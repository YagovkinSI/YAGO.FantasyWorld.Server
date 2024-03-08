using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.ApiContracts.QuestApi.Models;
using YAGO.FantasyWorld.ApiContracts.QuestApi.Requests;
using YAGO.FantasyWorld.Server.Application.Quests;

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

        [HttpPost]
        [Route("setQuestOption")]
        public async Task<string> SetQuestOption(SetQuestOptionRequest setQuestOptionRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _questService.SetQuestOption(HttpContext.User, setQuestOptionRequest.QuestId, setQuestOptionRequest.QuestOptionId, cancellationToken);
        }
    }
}
