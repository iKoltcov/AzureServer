using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceController : ControllerBase
    {
        private readonly IInstanceService instanceService;
        
        public InstanceController(IInstanceService instanceService)
        {
            this.instanceService = instanceService;
        }
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return instanceService.GetInstanceId();
        }
    }
}