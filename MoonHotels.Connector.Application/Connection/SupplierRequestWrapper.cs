namespace MoonHotels.Connector.Application.Connection;

public class SupplierRequestWrapper<TRequest>
{
    public TRequest Request { get; set; }

    public IDictionary<string, string> CustomHeaders { get; set; }

    public Uri Uri { get; set; }

    public HttpMethod HttpMethod { get; set; }
    public string Charset { get; set; }

    public int RequestId { get; set; }

    public HashSet<uint> AcceptedStatusCodes { get; set; }


    public SupplierRequestWrapper(
        TRequest request,
        Uri uri,
        HttpMethod httpMethod = default,
        IDictionary<string, string> customHeaders = default,
        string charset = default,
        bool requirePci = true)
    {
        Request = request;
        CustomHeaders = customHeaders;
        Uri = uri;
        HttpMethod = httpMethod ?? HttpMethod.Post;
        Charset = charset;
    }
}