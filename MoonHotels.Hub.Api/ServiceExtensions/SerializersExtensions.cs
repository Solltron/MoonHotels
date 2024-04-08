using System.Text.Json;
using MoonHotels.Hub.Application.Serializers;
using MoonHotels.Hub.Application.Serializers.Json;

namespace MoonHotels.Hub.Api.ServiceExtensions;

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