using System.Text.Json;

namespace HotelLegs.Connector.Common;

public static class JsonConfigurator
{
    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
    }
}