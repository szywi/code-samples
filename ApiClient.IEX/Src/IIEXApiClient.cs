using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.ApiClient.IEX.Dto;

namespace Domain.ApiClient.IEX
{
    public interface IIEXApiClient
    {
        Task<IEXCompanyDto> GetCompanyData(string stockSymbol, CancellationToken cancellationToken);

        Task<List<IEXCompanyDto>> GetCompaniesData(IEnumerable<string> stockSymbols, CancellationToken cancellationToken);
    }
}