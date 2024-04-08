using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoonHotels.Connector.Application.Connection;
using MoonHotels.Connector.Application.Connection.SupplierConnectionService;
using MoonHotels.Connector.Domain.Contracts.ErrorMessage;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;
using MoonHotels.Hub.Options;

namespace MoonHotels.Hub.Controllers;

[Route("api")]
[ApiController]
public class SearchController : Controller
{
    private readonly IOptions<ConnectorRequestConfig> _connectorsEndpoints;
    private readonly IConnectionService<SearchRequest, SearchResponse> _connectionService;

    public SearchController(IConnectionService<SearchRequest, SearchResponse> connectionService, IOptions<ConnectorRequestConfig> connectorsEndpoints)
    {
        _connectionService = connectionService;
        _connectorsEndpoints = connectorsEndpoints;
    }
    
    [HttpPost("search", Name = "HubSearch")]
    public async Task<SearchResponse> SearchAsync([FromBody] SearchRequest request,
        CancellationToken cancellationToken)
    {
        ConcurrentDictionary<int, Room> combinedRooms = new();
        ConcurrentBag<ErrorMessage> errorsBag = new();
        
        var connectorEndpoints = _connectorsEndpoints.Value.ConnectorEndpoints;
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = _connectorsEndpoints.Value.MaxParallelTransactions
        };
        await Parallel.ForEachAsync(connectorEndpoints, parallelOptions, async (connectorEndpoint, _) =>
        {
            var result = await ProxyToConnector($"{connectorEndpoint}/Search/search", request, cancellationToken);
            if (result.ErrorMessages?.Any() is true)
            {
                foreach (var resultErrorMessage in result.ErrorMessages)
                {
                    errorsBag.Add(resultErrorMessage);
                }
            }

            if (result.Rooms != null)
            {
                CombineRooms(result, combinedRooms);
            }
        });

        if (combinedRooms.Count == 0 && errorsBag.Any())
        {
            return new SearchResponse(default, errorsBag.ToList());
        }
        return new SearchResponse(combinedRooms.Values.ToList(), default);
    }

    private static void CombineRooms(SearchResponse result, ConcurrentDictionary<int, Room> combinedRooms)
    {
        foreach (var room in result.Rooms)
        {
            combinedRooms.AddOrUpdate(room.RoomId, room, (id, existingRoom) =>
            {
                // By default we check if the new room has cheaper rates
                if (room.Rates.Sum(rate => rate.Price) < existingRoom.Rates.Sum(rate => rate.Price))
                {
                    return room;
                }
                else
                {
                    return existingRoom;
                }
            });
        }
    }

    private async Task<SearchResponse> ProxyToConnector(
        string url,
        SearchRequest request,
        CancellationToken cancellationToken)
    {
        var requestWrapper = new SupplierRequestWrapper<SearchRequest>(request, new Uri(url));
        try
        {
            var response = await _connectionService.SendAsync(requestWrapper,
                cancellationToken);
            return response.Response;
        }
        catch (Exception ex)
        {
            return default;
        }
    }
}