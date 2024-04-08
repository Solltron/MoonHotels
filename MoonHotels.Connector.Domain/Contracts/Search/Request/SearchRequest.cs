namespace MoonHotels.Hub.Domain.Contracts.Search.Request;

public class SearchRequest
{
    public int hotelId { get; set; }
    public string checkIn { get; set; }
    public string checkOut { get; set; }
    public int numberOfGuests { get; set; }
    public int numberOfRooms { get; set; }
    public string currency { get; set; }
}