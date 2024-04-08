using MoonHotels.Hub.Application.Connection;
using MoonHotels.Hub.Application.Operations.Search;
using MoonHotels.Hub.Application.Pipelines;
using MoonHotels.Hub.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Hub.Application.Operations;

public interface IHubOperation<THubRequest, THubResponse, TSupplierRequest, TSupplierResponse>
{
    public IEnumerable<SupplierRequestWrapper<TSupplierRequest>> BuildRequests(THubRequest hubRequest);
    
    public THubResponse ParseResponses(
        THubRequest hubRequest,
        IEnumerable<SupplierResponseWrapper<TSupplierResponse>> supplierResponses);

    public bool TryValidateRequest(THubRequest hubRequest, out IEnumerable<ErrorMessage> errorMessages);

    public bool TryValidateResponse(IEnumerable<TSupplierResponse> hubResponse, out IEnumerable<ErrorMessage> errorMessages);
}