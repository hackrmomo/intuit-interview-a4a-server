using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Bolt.Services
{
    public interface ITempService
    {
        public Task<string> BasicOperation();
    }

    public class TempService : ITempService
    {
        public TempService(IConfiguration config)
        {
        }

        public async Task<string> BasicOperation () {
            return "Bolt Services Active";
        }
    }

}