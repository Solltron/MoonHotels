namespace MoonHotels.Hub.Application.Connection;

public interface ISupplierConnectionService<TSupplierRequest, TSupplierResponse>
{
    public Task<SupplierResponseWrapper<TSupplierResponse>> SendAsync(
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper,
        CancellationToken cancellationToken);
}