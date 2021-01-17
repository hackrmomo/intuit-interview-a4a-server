using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bolt.Controllers
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
