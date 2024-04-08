namespace MoonHotels.Connector.Application.Connection.RequestSender;

public interface IRequestSenderService<TSupplierRequest, TSupplierResponse>
{
    public Task<SupplierConnectionWrapper> SendAsync(
        MemoryStream requestStream,
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper,
        CancellationToken cancellationToken);  
}