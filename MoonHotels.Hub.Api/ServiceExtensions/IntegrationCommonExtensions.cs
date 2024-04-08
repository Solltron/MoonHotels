using Microsoft.Extensions.DependencyInjection.Extensions;
using MoonHotels.Hub.Api.Controllers;
using MoonHotels.Hub.Application.Connection;
using MoonHotels.Hub.Application.Operations;
using MoonHotels.Hub.Application.Pipelines;
using MoonHotels.Hub.Application.Pipelines.Steps;

namespace MoonHotels.Hub.Api.ServiceExtensions;

public static class IntegrationCommonExtensions
{

    public static void AddCommonServices(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddControllersAsServices();
    }
    public static void AddControllersSingletons(this IServiceCollection services)
    {
        services.AddSingleton<SearchController>();
    }
    
    public static IServiceCollection AddPipeline<TPipeline, TRq, TRs>(this IServiceCollection services)
        where TPipeline : class, IPipeline<TRq, TRs>
    {
        return services.AddSingleton<TPipeline>();
    }
    
    public static IServiceCollection AddDefaultOperationServices<
            TOperation, TConnectorRequest, TConnectorResponse, TSupplierRequest, TSupplierResponse>
        (this IServiceCollection services)
        where TOperation : class, IHubOperation<TConnectorRequest, TConnectorResponse,
            TSupplierRequest, TSupplierResponse>
    {
            return
                services
                    .AddSingleton<IOperationStep<TConnectorRequest, TConnectorResponse>,
                        OperationStep<TConnectorRequest, TConnectorResponse, TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IHubOperation<TConnectorRequest,
                        TConnectorResponse, TSupplierRequest, TSupplierResponse>, TOperation>()
                    .AddSingleton<ISupplierConnectionService<TSupplierRequest, TSupplierResponse>,
                        SupplierConnectionService<TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IRequestSenderService<TSupplierRequest, TSupplierResponse>,
                        HttpClientSupplierRequestSenderService<TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IHttpClientFactory,
                        FakeHttpClientFactory>();
    }
}