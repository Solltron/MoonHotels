using HotelLegs.Connector.Common;
using HotelLegs.Connector.Search.Models.Request;
using HotelLegs.Connector.Search.Models.Response;
using HotelLegs.Connector.Search.Operations;
using MoonHotels.Hub.Api.ServiceExtensions;

namespace HotelLegs.Connector.Search;

public static class SearchExtensions
{
    public static void AddSearchServices(this IServiceCollection services)
    {
        services.AddJsonSerializer<HotelLegsSearchRequest, HotelLegsSearchResponse>(JsonConfigurator.ConfigureJsonOptions);
        services.AddSearchOperation<SearchOperation, HotelLegsSearchRequest, HotelLegsSearchResponse>();
    }
}