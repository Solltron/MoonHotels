using System.Net;

namespace MoonHotels.Connector.Application.Connection.HttpClient;

public class FakeHttpClient : HttpMessageHandler
{
    private const string AVAIL_RESPONSE = """
                                          {
                                              "results": [
                                                  {
                                                      "room": 1,
                                                      "meal": 1,
                                                      "canCancel": false,
                                                      "price": 123.48
                                                  },
                                                  {
                                                      "room": 1,
                                                      "meal": 1,
                                                      "canCancel": true,
                                                      "price": 150.00
                                                  },
                                                  {
                                                      "room": 2,
                                                      "meal": 1,
                                                      "canCancel": false,
                                                      "price": 148.25
                                                  },
                                                  {
                                                      "room": 2,
                                                      "meal": 2,
                                                      "canCancel": false,
                                                      "price": 165.38
                                                  }
                                              ]
                                          }
                                          """;
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            { Content = new StringContent(AVAIL_RESPONSE) });
    }
}