using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bolt.Services;

namespace Bolt.Controllers
{
    [ApiController]
    [Route("temp")]
    public class TempController : ControllerBase
    {

        private ITempService _tempService;

        public TempController(ITempService tempService)
        {
            _tempService = tempService;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetTemp()
        {
            var result = await _tempService.BasicOperation();
            return Ok(result);
        }
    }
}
