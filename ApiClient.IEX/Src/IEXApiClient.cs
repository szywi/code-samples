using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Domain.ApiClient.IEX.Dto;
using Domain.ApiClient.IEX.Infrastructure;

namespace Domain.ApiClient.IEX
{
    public class IEXApiClient : IIEXApiClient
    {
        private const string Version = "stable";
        private const string BaseUrl = "https://cloud.iexapis.com";
        private static readonly string Token = ConfigurationManager.AppSettings["IEXCloudApiToken"];

        private readonly HttpClient httpClient;

        public IEXApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEXCompanyDto> GetCompanyData(string stockSymbol, CancellationToken cancellationToken)
        {
            var url = $"{BaseUrl}/{Version}/stock/{stockSymbol}/company?token={Token}";

            var response = await this.httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            var result = await EnsureSuccessAndDeserialize<IEXCompanyDto>(response).ConfigureAwait(false);

            return result;
        }

        public async Task<List<IEXCompanyDto>> GetCompaniesData(IEnumerable<string> stockSymbols, CancellationToken cancellationToken)
        {
            var url = $"{BaseUrl}/{Version}/stock/market/batch?token={Token}&types=company&symbols=";
            var arguments = string.Join(",", stockSymbols);

            var response = await this.httpClient.GetAsync($"{url}{arguments}", cancellationToken).ConfigureAwait(false);
            var result = await EnsureSuccessAndDeserialize<IEXCompaniesResponseDto>(response).ConfigureAwait(false);

            return result.Companies;
        }

        private static async Task<TDto> EnsureSuccessAndDeserialize<TDto>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var rawObject = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<TDto>(rawObject, new IEXBatchResponseConverter());
        }
    }
}