using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Bolt.Services
{
    public interface ITempService
    {
        public Task<string> BasicOperation();
    }

    public class TempService : ITempService
    {
        private MySqlConnection _conn;
        public TempService(IConfiguration config)
        {
            _conn = new MySqlConnection(config.GetSection("MySQLConnectionString").Value);
        }

        public async Task<string> BasicOperation () {
            using (_conn) {
                await _conn.OpenAsync();
                var command = new MySqlCommand("SELECT 1", _conn);
                var result = await command.ExecuteScalarAsync();
            }
            return "Bolt Services Active";
        }
    }

}