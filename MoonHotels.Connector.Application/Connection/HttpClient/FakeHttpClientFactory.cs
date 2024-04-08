namespace MoonHotels.Connector.Application.Connection.HttpClient;

public class FakeHttpClientFactory : IHttpClientFactory
{
    public System.Net.Http.HttpClient CreateClient(string name)
    {
        return new System.Net.Http.HttpClient(new FakeHttpClient());
    }
}