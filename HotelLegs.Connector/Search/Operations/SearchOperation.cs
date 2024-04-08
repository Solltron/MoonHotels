using HotelLegs.Connector.Search.Models.Request;
using HotelLegs.Connector.Search.Models.Response;
using MoonHotels.Hub.Application.Operations.Search;
using MoonHotels.Hub.Domain.Contracts.ErrorMessage;
using MoonHotels.Hub.Domain.Contracts.Search.Request;

namespace HotelLegs.Connector.Search.Operations;

public partial class SearchOperation : ISearchOperation<HotelLegsSearchRequest, HotelLegsSearchResponse>
{
    public bool TryValidateRequest(SearchRequest request, out IEnumerable<ErrorMessage> errorMessages)
    {
        errorMessages = default;
        return true;
    }

    public bool TryValidateResponse(IEnumerable<HotelLegsSearchResponse> response, out IEnumerable<ErrorMessage> errorMessages)
    {
        errorMessages = default;
        return true;
    }
}