using System.Text.Json.Serialization;

namespace MoonHotels.Hub.Domain.Contracts.Search.Response;

public class SearchResponse
{
    [JsonPropertyName("rooms")]
    public List<Room> Rooms { get; set; }
    [JsonPropertyName("errors")]
    public IEnumerable<ErrorMessage.ErrorMessage> ErrorMessages { get; init; }
    
    private SearchResponse()
    {
    }

    public SearchResponse(List<Room> rooms, IEnumerable<ErrorMessage.ErrorMessage> errorMessages = null)
    {
        Rooms = rooms;
        ErrorMessages = errorMessages;
    }
    
    public static SearchResponse BuildErrorResponse(IEnumerable<ErrorMessage.ErrorMessage> errorMessages)
    {
        return new SearchResponse(null, errorMessages);
    }

    public static SearchResponse BuildErrorResponse(ErrorMessage.ErrorMessage errorMessage)
    {
        return new SearchResponse(null, new[] { errorMessage });
    }
}