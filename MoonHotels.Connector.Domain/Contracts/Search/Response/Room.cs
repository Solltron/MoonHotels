using System.Text.Json.Serialization;

namespace MoonHotels.Hub.Domain.Contracts.Search.Response;

public class Room
{
    [JsonPropertyName("roomId")]
    public int RoomId { get; set; }

    [JsonPropertyName("rates")]
    public List<Rate> Rates { get; set; }
}