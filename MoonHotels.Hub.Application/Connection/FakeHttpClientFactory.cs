namespace MoonHotels.Hub.Application.Connection;

public class FakeHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient(new FakeHttpClient());
    }
}