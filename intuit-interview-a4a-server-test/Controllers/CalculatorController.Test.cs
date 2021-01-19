using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Bolt.Models;
using Newtonsoft.Json;
using System;
using System.Text;
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
        public async Task Post_Calculate_Should_Fail_If_No_Currency()
        {
            List<ITransactionEntry> mockedTransactions = new List<ITransactionEntry>();
            mockedTransactions.Add(new ITransactionEntry {Account = "Acc1", AccountTerm = "short", MonthlyPayment = null, Type = "asset", Value = 7000});
            mockedTransactions.Add(new ITransactionEntry {Account = "Acc2", AccountTerm = "short", MonthlyPayment = null, Type = "liability", Value = 3000});
            

            var stringContent = new StringContent(JsonConvert.SerializeObject(mockedTransactions), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/calculator/calculate", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Post_Calculate_Should_Succeed_If_No_Transactions()
        {
            List<ITransactionEntry> mockedTransactions = new List<ITransactionEntry>();

            var stringContent = new StringContent(JsonConvert.SerializeObject(mockedTransactions), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/calculator/calculate?conversionType=CAD_CAD", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Be("{\"totalAssets\":0,\"totalLiabilities\":0,\"netWorth\":0,\"transactions\":[]}");
        }

        [Fact]
        public async Task Post_Calculate_Should_Succeed_With_3_Transactions()
        {
            List<ITransactionEntry> mockedTransactions = new List<ITransactionEntry>();
            var line1 = new ITransactionEntry {Account = "Acc1", AccountTerm = "short", MonthlyPayment = null, Type = "asset", Value = 7000};
            var line2 = new ITransactionEntry {Account = "Acc2", AccountTerm = "short", MonthlyPayment = null, Type = "liability", Value = 3000};
            var line3 = new ITransactionEntry {Account = "Acc3", AccountTerm = "long", MonthlyPayment = null, Type = "liability", Value = 3000};
            mockedTransactions.Add(line1);
            mockedTransactions.Add(line2);
            mockedTransactions.Add(line3);

            var stringContent = new StringContent(JsonConvert.SerializeObject(mockedTransactions), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/calculator/calculate?conversionType=CAD_CAD", stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsStringAsync()).Should().Contain("\"totalAssets\":7000,\"totalLiabilities\":6000,\"netWorth\":1000");
        }

    }
}