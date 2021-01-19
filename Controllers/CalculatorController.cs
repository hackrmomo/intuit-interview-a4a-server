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
        public async Task<OkObjectResult> Calculate([FromBody] IEnumerable<ITransactionEntry> transactions, string conversionType)
        {
            var convertedTransactions = transactions;

            var currencies = conversionType.Split("_");
            if (currencies.Count() == 2)
            {
                var from = currencies.First();
                var to = currencies.Last();

                var conversionRate = await _calculatorService.GetConversionRate(from, to);

                foreach (var transaction in convertedTransactions) {
                    transaction.Value *= conversionRate;
                    if (transaction.MonthlyPayment != null) {
                        transaction.MonthlyPayment *= conversionRate;
                    }
                }
            }

            var totalAssets = _calculatorService.CalculateTotal(convertedTransactions.Where(a => a.Type == "asset").Select(a => a.Value));
            var totalLiabilities = _calculatorService.CalculateTotal(convertedTransactions.Where(a => a.Type == "liability").Select(a => a.Value));
            var netWorth = totalAssets - totalLiabilities;

            return Ok(new
            {
                totalAssets,
                totalLiabilities,
                netWorth,
                transactions = convertedTransactions
            });
        }
    }
}
