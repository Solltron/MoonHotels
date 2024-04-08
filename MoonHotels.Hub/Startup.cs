using MoonHotels.Connector.Api.ServiceExtensions;
using MoonHotels.Connector.Application.Connection.RequestSender;
using MoonHotels.Connector.Application.Connection.SupplierConnectionService;
using MoonHotels.Connector.Application.Serializers.Json;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;
using MoonHotels.Hub.Options;

namespace MoonHotels.Hub;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddJsonSerializer<SearchRequest, SearchResponse>(JsonConfiguratorDefault.ConfigureJsonOptions);
        services
            .AddSingleton<IConnectionService<SearchRequest, SearchResponse>,
                ConnectionService<SearchRequest, SearchResponse>>();
        services
            .AddSingleton<IRequestSenderService<SearchRequest, SearchResponse>, HttpClientSupplierRequestSenderService<SearchRequest, SearchResponse>>();
        services.AddHttpClient();
        services.Configure<ConnectorRequestConfig>(Configuration.GetSection("ConnectorRequestConfig"));
    }
    
}