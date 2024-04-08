using MoonHotels.Connector.Api.ServiceExtensions;
using MoonHotels.Connector.Application.Connection.RequestSender;
using MoonHotels.Connector.Application.Connection.SupplierConnectionService;
using MoonHotels.Connector.Application.Serializers.Json;
using MoonHotels.Hub;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;
using MoonHotels.Hub.Options;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(delegate(IEndpointRouteBuilder builder)
{
    builder.MapControllers();
});

app.UseAuthorization();

app.Run();