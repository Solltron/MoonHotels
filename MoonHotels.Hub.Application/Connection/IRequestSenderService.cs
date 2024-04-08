namespace MoonHotels.Hub.Application.Connection;

public interface IRequestSenderService<TSupplierRequest, TSupplierResponse>
{
    public Task<SupplierConnectionWrapper> SendAsync(
        MemoryStream requestStream,
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper,
        CancellationToken cancellationToken);  
}