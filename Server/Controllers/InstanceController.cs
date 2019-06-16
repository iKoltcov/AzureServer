using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceController : ControllerBase
    {
        private readonly IInstanceService _instanceService;
        
        public InstanceController(IInstanceService instanceService)
        {
            _instanceService = instanceService;
        }
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return _instanceService.GetInstanceId();
        }
    }
}