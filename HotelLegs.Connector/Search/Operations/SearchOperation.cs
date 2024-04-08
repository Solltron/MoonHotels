using HotelLegs.Connector.Search.Models.Request;
using HotelLegs.Connector.Search.Models.Response;
using MoonHotels.Connector.Application.Operations.Search;
using MoonHotels.Connector.Domain.Contracts.ErrorMessage;
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
        var supplierResponse = response.First();
        if (supplierResponse.Results?.Count <= 0)
        {
            errorMessages = new[] { ErrorMessage.BuildNoResultsFoundError() };
            return false;
        }
        errorMessages = default;
        return true;
    }
}