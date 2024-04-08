using System.Text.Json;
using MoonHotels.Connector.Application.Serializers;
using MoonHotels.Connector.Application.Serializers.Json;

namespace MoonHotels.Connector.Api.ServiceExtensions;

public static class SerializersExtensions
{
    public static IServiceCollection AddJsonSerializer<TRequest, TResponse>(this IServiceCollection services,
        Action<JsonSerializerOptions> configure)
    {
        var options = new JsonSerializerOptions();
        configure(options);

        services
            .AddSingleton<ISerializer<TRequest, TResponse>>(new JsonSerializer<TRequest, TResponse>(options));

        return services;
    }
}