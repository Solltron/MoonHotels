using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Hub.Application.Operations.Search;

public interface ISearchOperation<TSupplierRequest, TSupplierResponse> :
    IHubOperation<SearchRequest, SearchResponse, TSupplierRequest, TSupplierResponse>
{
}