namespace MoonHotels.Connector.Application.Connection;

public class SupplierResponseWrapper<TResponse>
{
    public TResponse Response { get; internal set; }

    public IDictionary<string, string> ResponseHeaders { get; internal set; }

    public uint HttpStatusCode { get; internal set; }

    public Exception Exception { get; set; }

    public int RequestId { get; init; }
}