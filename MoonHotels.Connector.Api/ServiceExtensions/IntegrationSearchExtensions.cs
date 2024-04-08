using MoonHotels.Connector.Application.Operations;
using MoonHotels.Connector.Application.Operations.Search;
using MoonHotels.Connector.Application.Pipelines;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Connector.Api.ServiceExtensions;

public static class IntegrationSearchExtensions
{
    public static IServiceCollection AddSearchOperation<TSearch, TSupplierRequest, TSupplierResponse>(
        this IServiceCollection services)
        where TSearch : class, ISearchOperation<TSupplierRequest, TSupplierResponse>
    {
        return
            services
                .AddPipeline<SearchPipeline, SearchRequest, SearchResponse>()
                .AddSearchOperationServices<TSearch, TSupplierRequest, TSupplierResponse>();
    }
    
    private static IServiceCollection AddSearchOperationServices<TSearch, TSupplierRequest, TSupplierResponse>
        (this IServiceCollection services)
        where TSearch : class, IConnectorOperation<SearchRequest,
            SearchResponse, TSupplierRequest, TSupplierResponse>
    {
        return
            services.AddDefaultOperationServices<
                TSearch,
                SearchRequest,
                SearchResponse,
                TSupplierRequest,
                TSupplierResponse>();
    }
}