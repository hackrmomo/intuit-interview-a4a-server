using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Bolt.Models;
using Newtonsoft.Json;
using FluentAssertions;

namespace Bolt.Controllers.Test
{
    public class CalculatorControllerTest : IClassFixture<WebApplicationFactory<CalculatorController>>
    {
        public HttpClient Client { get; }
        public CalculatorControllerTest(WebApplicationFactory<CalculatorController> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Post_Calculate_Should_Succeed()
        {
            List<ITransactionEntry> mockedTransactions = new List<ITransactionEntry>();
            mockedTransactions.Add(new ITransactionEntry {Account = "Acc1", AccountTerm = "short", MonthlyPayment = null, Type = "asset", Value = 7000});
            mockedTransactions.Add(new ITransactionEntry {Account = "Acc2", AccountTerm = "short", MonthlyPayment = null, Type = "liability", Value = 3000});
            

            var stringContent = new StringContent(JsonConvert.SerializeObject(mockedTransactions));
            var response = await Client.PostAsync("/calculator/calculate", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}