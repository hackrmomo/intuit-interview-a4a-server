using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Bolt.Services
{
    public interface ICalculatorService
    {
        public double CalculateTotal(IEnumerable<double> numList);
        public Task<double> GetConversionRate(string from, string to);
    }

    public class CalculatorService : ICalculatorService
    {
        private HttpClient _client;
        private IConfiguration _config;

        public CalculatorService(IConfiguration config)
        {
            this._client = new HttpClient();
            this._config = config;
        }

        public double CalculateTotal(IEnumerable<double> numList)
        {
            return numList.Sum();
        }

        public async Task<double> GetConversionRate(string from, string to)
        {
            var url = $"{_config.GetSection("CurrencyConversionEndPoint").Value}?q={from}_{to}&compact=ultra&apiKey={_config.GetSection("CurrencyConversionApiKey").Value}";
            var result = await _client.GetAsync(url);
            double conversionRate = 1.0;
            if (result.IsSuccessStatusCode)
            {
                try
                {
                    IDictionary<string, double> jsonResult = JsonConvert.DeserializeObject<IDictionary<string, double>>(await result.Content.ReadAsStringAsync());
                    jsonResult.TryGetValue($"{from}_{to}", out conversionRate);
                }
                catch (Exception e)
                {
                    // log failure somehow and potentially alert front-end that conversion was not successful
                }
            }
            return conversionRate;
        }
    }

}