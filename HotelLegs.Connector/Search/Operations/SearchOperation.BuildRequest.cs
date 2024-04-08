using System.Globalization;
using HotelLegs.Connector.Search.Models.Request;
using MoonHotels.Connector.Application.Connection;
using MoonHotels.Hub.Domain.Contracts.Search.Request;

namespace HotelLegs.Connector.Search.Operations;

public partial class SearchOperation
{
    public IEnumerable<SupplierRequestWrapper<HotelLegsSearchRequest>> BuildRequests(SearchRequest request)
    {
        //if more requests are required, they can be build here and send into the array
        HotelLegsSearchRequest supplierRequest = BuildSupplierRequest(request);

        var requestWrapper = new SupplierRequestWrapper<HotelLegsSearchRequest>(supplierRequest, new Uri("http://fakeUrl"));

        return new[] { requestWrapper };
    }

    private HotelLegsSearchRequest BuildSupplierRequest(SearchRequest request)
    {
        return new HotelLegsSearchRequest()
        {
            Hotel = request.hotelId,
            CheckInDate = request.checkIn,
            NumberOfNights = CalculateNumberOfNights(request.checkIn, request.checkOut),
            Rooms = request.numberOfRooms,
            Currency = request.currency
        };
    }

    private int CalculateNumberOfNights(string searchRequestCheckIn, string searchRequestCheckOut)
    {
        DateTime checkInDate = DateTime.ParseExact(searchRequestCheckIn, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime checkOutDate = DateTime.ParseExact(searchRequestCheckOut, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        
        TimeSpan timeSpan = checkOutDate - checkInDate;
        return timeSpan.Days;
    }
}