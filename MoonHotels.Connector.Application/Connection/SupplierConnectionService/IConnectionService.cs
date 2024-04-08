namespace MoonHotels.Connector.Application.Connection.SupplierConnectionService;

public interface IConnectionService<TSupplierRequest, TSupplierResponse>
{
    public Task<SupplierResponseWrapper<TSupplierResponse>> SendAsync(
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper,
        CancellationToken cancellationToken);
}