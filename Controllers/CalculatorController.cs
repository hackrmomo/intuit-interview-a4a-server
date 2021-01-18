using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bolt.Services;
using System.Collections.Generic;
using Bolt.Models;
using System.Linq;

namespace Bolt.Controllers
{
    [ApiController]
    [Route("calculator")]
    public class CalculatorController : ControllerBase
    {

        private ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService CalculatorService)
        {
            _calculatorService = CalculatorService;
        }

        [HttpPost]
        [Route("calculate")]
        public OkObjectResult Calculate([FromBody] IEnumerable<ITransactionEntry> transactions)
        {
            var totalAssets = _calculatorService.CalculateTotal(transactions.Where(a => a.Type == "asset").Select(a => a.Value));
            var totalLiabilities = _calculatorService.CalculateTotal(transactions.Where(a => a.Type == "liability").Select(a => a.Value));
            var netWorth = totalAssets - totalLiabilities;
            return Ok(new {
                totalAssets,
                totalLiabilities,
                netWorth
            });
        }
    }
}
