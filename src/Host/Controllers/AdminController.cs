using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Admin;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("fillDatabase")]
        public async Task FillDatabase(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _adminService.FillDatabase(cancellationToken);
        }
    }
}
