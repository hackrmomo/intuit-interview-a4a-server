using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MavenlinkUnify.WebAdmin.Controllers
{
    [ApiController]
    [Route("temp")]
    public class TempController : ControllerBase
    {

        [HttpGet]
        public OkObjectResult GetTemp()
        {
            return Ok("Bolt Services Active");
        }
    }
}
