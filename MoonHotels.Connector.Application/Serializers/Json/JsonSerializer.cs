using System.Net.Http.Headers;
using System.Text.Json;

namespace MoonHotels.Connector.Application.Serializers.Json;

public class JsonSerializer<TRequest, TResponse> : ISerializer<TRequest, TResponse>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonSerializer(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }
    public virtual async Task<TResponse> DeserializeAsync(Stream stream)
    {
        await using (stream)
        {
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, _jsonSerializerOptions);
        }
    }

    public virtual async Task<MemoryStream> SerializeAsync(TRequest data)
    {
        var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, data, _jsonSerializerOptions);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }

    public MediaTypeHeaderValue GetContentType()
    {
        return new MediaTypeHeaderValue("application/json");
    }
}