using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Connector.Application.Operations.Search;

public interface ISearchOperation<TSupplierRequest, TSupplierResponse> :
    IConnectorOperation<SearchRequest, SearchResponse, TSupplierRequest, TSupplierResponse>
{
}