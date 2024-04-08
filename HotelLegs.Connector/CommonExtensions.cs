using HotelLegs.Connector.Search;

namespace HotelLegs.Connector;

public static class CommonExtensions
{
    public static void AddOperationServices(this IServiceCollection services)
    {
        services.AddSearchServices();
    }
}