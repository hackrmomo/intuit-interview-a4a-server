using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Bolt.Services
{
    public interface ICalculatorService
    {
        public double CalculateTotal(IEnumerable<double> numList);
    }

    public class CalculatorService : ICalculatorService
    {
        public double CalculateTotal (IEnumerable<double> numList) {
            return numList.Sum();
        }
    }

}