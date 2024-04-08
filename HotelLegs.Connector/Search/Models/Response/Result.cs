using System.Text.Json.Serialization;

namespace HotelLegs.Connector.Search.Models.Response;

public class Result
{
    [JsonPropertyName("room")]
    public int Room { get; set; }

    [JsonPropertyName("meal")]
    public int Meal { get; set; }

    [JsonPropertyName("canCancel")]
    public bool CanCancel { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }
}