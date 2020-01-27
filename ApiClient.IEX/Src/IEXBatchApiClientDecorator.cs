using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.ApiClient.IEX.Dto;

namespace Domain.ApiClient.IEX
{
    public class IEXBatchApiClientDecorator : IIEXApiClient
    {
        private const int BatchItemLimit = 100;
        private readonly IIEXApiClient iexApiClient;

        public IEXBatchApiClientDecorator(IIEXApiClient iexApiClient)
        {
            this.iexApiClient = iexApiClient;
        }

        public Task<IEXCompanyDto> GetCompanyData(string stockSymbol, CancellationToken cancellationToken)
        {
            return this.iexApiClient.GetCompanyData(stockSymbol, cancellationToken);
        }

        public async Task<List<IEXCompanyDto>> GetCompaniesData(IEnumerable<string> stockSymbols, CancellationToken cancellationToken)
        {
            var stockList = stockSymbols.ToList();

            if (stockList.Count < BatchItemLimit)
            {
                return await this.iexApiClient.GetCompaniesData(stockList, cancellationToken).ConfigureAwait(false);
            }

            var results = new List<IEXCompanyDto>();

            var processed = 0;
            var numberOfRequests = stockList.Count / BatchItemLimit;

            for (var i = 0; i <= numberOfRequests; ++i)
            {
                if (stockList.Count == processed)
                {
                    break;
                }

                var symbols = stockList.Skip(processed).Take(BatchItemLimit);

                var companies = await this.iexApiClient.GetCompaniesData(symbols, cancellationToken).ConfigureAwait(false);

                results.AddRange(companies);

                processed = (i + 1) * BatchItemLimit;
            }

            return results;
        }
    }
}