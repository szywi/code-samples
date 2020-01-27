using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Domain.ApiClient.IEX;
using Domain.ApiClient.IEX.Constants;
using Xunit;

namespace Domain.IntegrationTest.ApiClient.IEX
{
    public class IEXApiClientTest : IDisposable
    {
        private const string NyseStockSymbol = "HOG";
        private const string NasdaqStockSymbol = "FB";

        private readonly HttpClient httpClient;
        private readonly IEXApiClient testee;

        public IEXApiClientTest()
        {
            this.httpClient = new HttpClient();
            this.testee = new IEXApiClient(this.httpClient);
        }

        public void Dispose()
        {
            this.httpClient?.Dispose();
        }

        [Fact]
        public async Task GetCompaniesData_SmokeTest()
        {
            var stocks = new List<string> { NasdaqStockSymbol, NyseStockSymbol };

            var results = await this.testee.GetCompaniesData(stocks, CancellationToken.None).ConfigureAwait(false);

            results.Should().HaveCount(stocks.Count);

            var nasdaqStock = results.First();
            nasdaqStock.Symbol.Should().Be(NasdaqStockSymbol);
            nasdaqStock.Exchange.Should().Be(IEXStockExchanges.NASDAQ);

            var nyseStock = results.Last();
            nyseStock.Symbol.Should().Be(NyseStockSymbol);
            nyseStock.Exchange.Should().Be(IEXStockExchanges.NYSE);
        }

        [Fact]
        public async Task GetCompanyData_StockIsListedOnNasdaq_NasdaqExchangeIsReturned()
        {
            var result = await this.testee.GetCompanyData(NasdaqStockSymbol, CancellationToken.None).ConfigureAwait(false);

            result.Exchange.Should().Be(IEXStockExchanges.NASDAQ);
        }

        [Fact]
        public async Task GetCompanyData_StockIsListedOnNyse_NyseExchangeIsReturned()
        {
            var result = await this.testee.GetCompanyData(NyseStockSymbol, CancellationToken.None).ConfigureAwait(false);

            result.Exchange.Should().Be(IEXStockExchanges.NYSE);
        }
    }
}