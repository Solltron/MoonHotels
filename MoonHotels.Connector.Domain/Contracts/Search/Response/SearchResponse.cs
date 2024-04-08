using System.Text.Json.Serialization;

namespace MoonHotels.Hub.Domain.Contracts.Search.Response;

public class SearchResponse
{
    [JsonPropertyName("rooms")]
    public List<Room> Rooms { get; set; }
    [JsonPropertyName("errors")]
    public IEnumerable<Connector.Domain.Contracts.ErrorMessage.ErrorMessage> ErrorMessages { get; init; }
    
    private SearchResponse()
    {
    }

    public SearchResponse(List<Room> rooms, IEnumerable<Connector.Domain.Contracts.ErrorMessage.ErrorMessage> errorMessages = null)
    {
        Rooms = rooms;
        ErrorMessages = errorMessages;
    }
    
    public static SearchResponse BuildErrorResponse(IEnumerable<Connector.Domain.Contracts.ErrorMessage.ErrorMessage> errorMessages)
    {
        return new SearchResponse(null, errorMessages);
    }

    public static SearchResponse BuildErrorResponse(Connector.Domain.Contracts.ErrorMessage.ErrorMessage errorMessage)
    {
        return new SearchResponse(null, new[] { errorMessage });
    }
}