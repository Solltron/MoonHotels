using System.Text.Json;

namespace MoonHotels.Connector.Application.Serializers.Json;

public static class JsonConfiguratorDefault
{
    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
    }
}