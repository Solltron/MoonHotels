using MoonHotels.Connector.Application.Connection;
using MoonHotels.Connector.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Connector.Application.Operations;

public interface IConnectorOperation<in TConnectorRequest, out TConnectorResponse, TSupplierRequest, TSupplierResponse>
{
    public IEnumerable<SupplierRequestWrapper<TSupplierRequest>> BuildRequests(TConnectorRequest hubRequest);
    
    public TConnectorResponse ParseResponses(
        TConnectorRequest hubRequest,
        IEnumerable<SupplierResponseWrapper<TSupplierResponse>> supplierResponses);

    public bool TryValidateRequest(TConnectorRequest hubRequest, out IEnumerable<ErrorMessage> errorMessages);

    public bool TryValidateResponse(IEnumerable<TSupplierResponse> hubResponse, out IEnumerable<ErrorMessage> errorMessages);
}