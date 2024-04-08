using HotelLegs.Connector;
using MoonHotels.Connector.Api.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersSingletons();
builder.Services.AddOperationServices();
builder.Services.AddCommonServices();

var app = builder.Build();
app.UseRouting();

app.UseEndpoints(delegate(IEndpointRouteBuilder builder)
{
    builder.MapControllers();
});
await app.RunAsync();