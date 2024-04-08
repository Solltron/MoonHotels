using Microsoft.AspNetCore.Mvc;
using MoonHotels.Connector.Application.Pipelines;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Connector.Api.Controllers.ConnectorControllers;

[Route("[controller]")]
[ApiController]
public class SearchController : Controller
{
    private readonly SearchPipeline _searchPipeline;

    public SearchController(SearchPipeline searchPipeline)
    {
        _searchPipeline = searchPipeline;
    }
    
    [HttpPost("search", Name = "SupplierSearch")]
    public async Task<SearchResponse> SearchAsync(
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken)
    {
        return await _searchPipeline.ProcessAsync(request, cancellationToken);
    }
}