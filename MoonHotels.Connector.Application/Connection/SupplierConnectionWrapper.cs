namespace MoonHotels.Connector.Application.Connection;

public class SupplierConnectionWrapper : IDisposable
{
    public Request RequestData { get; }

    public Response ResponseData { get; }

    public Exception Exception { get; }

    public bool Success => Exception is null;

    public SupplierConnectionWrapper(Request requestData, Response responseData, Exception exception)
    {
        RequestData = requestData;
        ResponseData = responseData;
        Exception = exception;
    }

    public class Request
    {
        public Request(MemoryStream requestMemoryStream,
            IDictionary<string, string> headers,
            HttpMethod httpMethod, string url)
        {
            RequestStream = requestMemoryStream;
            Headers = headers;
            HttpMethod = httpMethod;
            Url = url;
        }

        public MemoryStream RequestStream { get; }

        public IDictionary<string, string> Headers { get; }

        public HttpMethod HttpMethod { get; }

        public string Url { get; }
    }

    public class Response
    {
        public uint HttpStatusCode { get; }

        public MemoryStream ResponseStream { get; }

        public IDictionary<string, string> Headers { get; }

        public Response(
            MemoryStream responseStream,
            uint httpStatusCode,
            IDictionary<string, string> headers)
        {
            ResponseStream = responseStream;
            HttpStatusCode = httpStatusCode;
            Headers = headers;
        }
    }
    
    public void Dispose()
    {
        ResponseData?.ResponseStream?.Dispose();
        RequestData?.RequestStream?.Dispose();
    }
}