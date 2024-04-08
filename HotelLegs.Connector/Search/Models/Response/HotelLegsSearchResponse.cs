using System.Text.Json.Serialization;

namespace HotelLegs.Connector.Search.Models.Response;

public class HotelLegsSearchResponse
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
}