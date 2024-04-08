using System.Text.Json.Serialization;

namespace HotelLegs.Connector.Search.Models.Request;

public class HotelLegsSearchRequest
{
    [JsonPropertyName("hotel")]
    public int Hotel { get; set; }

    [JsonPropertyName("checkInDate")]
    public string CheckInDate { get; set; }

    [JsonPropertyName("numberOfNights")]
    public int NumberOfNights { get; set; }

    [JsonPropertyName("guests")]
    public int Guests { get; set; }

    [JsonPropertyName("rooms")]
    public int Rooms { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}