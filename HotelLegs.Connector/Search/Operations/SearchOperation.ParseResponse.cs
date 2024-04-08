using HotelLegs.Connector.Search.Models.Response;
using MoonHotels.Hub.Application.Connection;
using MoonHotels.Hub.Application.Operations.Search;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace HotelLegs.Connector.Search.Operations;

public partial class SearchOperation
{
    public SearchResponse ParseResponses(SearchRequest hubRequest, IEnumerable<SupplierResponseWrapper<HotelLegsSearchResponse>> supplierResponses)
    {
        var supplierResponse = supplierResponses.First().Response;

        return ParseSupplierResponse(supplierResponse);
    }

    private SearchResponse ParseSupplierResponse(HotelLegsSearchResponse supplierResponse)
    {
        var listRooms = new List<Room>();
        
        foreach (var result in supplierResponse.Results.GroupBy(results => results.Room))
        {
            listRooms.Add(ParseRoom(result));
        }

        return new SearchResponse(listRooms);
    }

    private Room ParseRoom(IGrouping<int, Result> resultGroup)
    {
        var room = new Room();
        room.RoomId = resultGroup.Key;
        room.Rates = new List<Rate>();
        
        foreach (var supplierRate in resultGroup)
        {
            var rate = new Rate()
            {
                MealPlanId = supplierRate.Meal,
                IsCancellable = supplierRate.CanCancel,
                Price = supplierRate.Price
            };
            room.Rates.Add(rate);
        }

        return room;
    }
}