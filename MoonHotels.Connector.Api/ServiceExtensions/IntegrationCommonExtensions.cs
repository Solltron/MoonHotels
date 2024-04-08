using MoonHotels.Connector.Api.Controllers.ConnectorControllers;
using MoonHotels.Connector.Application.Connection.HttpClient;
using MoonHotels.Connector.Application.Connection.RequestSender;
using MoonHotels.Connector.Application.Connection.SupplierConnectionService;
using MoonHotels.Connector.Application.Operations;
using MoonHotels.Connector.Application.Pipelines;
using MoonHotels.Connector.Application.Pipelines.Steps;

namespace MoonHotels.Connector.Api.ServiceExtensions;

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
        where TOperation : class, IConnectorOperation<TConnectorRequest, TConnectorResponse,
            TSupplierRequest, TSupplierResponse>
    {
            return
                services
                    .AddSingleton<IOperationStep<TConnectorRequest, TConnectorResponse>,
                        OperationStep<TConnectorRequest, TConnectorResponse, TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IConnectorOperation<TConnectorRequest,
                        TConnectorResponse, TSupplierRequest, TSupplierResponse>, TOperation>()
                    .AddSingleton<IConnectionService<TSupplierRequest, TSupplierResponse>,
                        ConnectionService<TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IRequestSenderService<TSupplierRequest, TSupplierResponse>,
                        HttpClientSupplierRequestSenderService<TSupplierRequest, TSupplierResponse>>()
                    .AddSingleton<IHttpClientFactory,
                        FakeHttpClientFactory>();
    }
}