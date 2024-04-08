using MoonHotels.Connector.Application.Serializers;

namespace MoonHotels.Connector.Application.Connection.RequestSender;

public class HttpClientSupplierRequestSenderService<TSupplierRequest, TSupplierResponse>
    : IRequestSenderService<TSupplierRequest, TSupplierResponse>
{
    private readonly ISerializer<TSupplierRequest, TSupplierResponse> _serializer;
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientSupplierRequestSenderService(
        ISerializer<TSupplierRequest, TSupplierResponse> connectorSerializer,
        IHttpClientFactory httpClientFactory)
    {
        _serializer = connectorSerializer;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SupplierConnectionWrapper> SendAsync(
        MemoryStream requestStream,
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper,
        CancellationToken cancellationToken)
    {
        SupplierConnectionWrapper.Request requestData = null;
        SupplierConnectionWrapper.Response responseData = null;
        Exception exception = null;

        try
        {
            using var httpRequest = BuildHttpRequestMessage(requestStream, supplierRequestWrapper);

            var client = _httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            await using var streamResponse = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);

            var responseStreamCopy = new MemoryStream();

            await streamResponse.CopyToAsync(responseStreamCopy, cancellationToken);

            responseStreamCopy.Seek(0, SeekOrigin.Begin);
            
            responseData = new SupplierConnectionWrapper.Response(
                responseStreamCopy,
                (uint)httpResponseMessage.StatusCode,
                null);
        }
        catch (Exception e)
        {
            exception = e;
        }

        return new SupplierConnectionWrapper(requestData, responseData, exception);
    }

    private HttpRequestMessage BuildHttpRequestMessage(
        System.IO.Stream requestStream,
        SupplierRequestWrapper<TSupplierRequest> providerRequestWrapper)
    {
        HttpRequestMessage res;
        if (providerRequestWrapper.HttpMethod.Method != HttpMethod.Get.Method)
        {
            res = new HttpRequestMessage(providerRequestWrapper.HttpMethod, providerRequestWrapper.Uri)
            {
                Content = new StreamContent(requestStream)
            };

            res.Content.Headers.ContentType = _serializer.GetContentType();
            if (!string.IsNullOrEmpty(providerRequestWrapper.Charset))
            {
                res.Content.Headers.ContentType!.CharSet = providerRequestWrapper.Charset;
            }
        }
        else
        {
            res = new HttpRequestMessage(providerRequestWrapper.HttpMethod, providerRequestWrapper.Uri);
        }

        AddCustomHeaders(res, providerRequestWrapper.CustomHeaders);

        return res;
    }

    private static void AddCustomHeaders(HttpRequestMessage res, IDictionary<string, string> customHeaders)
    {
        if (customHeaders == null || customHeaders.Count == 0)
        {
            return;
        }

        foreach (var (key, value) in customHeaders)
        {
            res.Headers.TryAddWithoutValidation(key, value);
        }
    }
}