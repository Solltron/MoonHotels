using Microsoft.AspNetCore.Mvc;
using MoonHotels.Hub.Application.Pipelines;
using MoonHotels.Hub.Domain.Contracts.Search.Request;
using MoonHotels.Hub.Domain.Contracts.Search.Response;

namespace MoonHotels.Hub.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : Controller
{
    private readonly SearchPipeline _searchPipeline;

    public SearchController(SearchPipeline searchPipeline)
    {
        _searchPipeline = searchPipeline;
    }
    
    [HttpPost("Search", Name = "Search")]
    public async Task<SearchResponse> SearchAsync(
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken)
    {
        return await _searchPipeline.ProcessAsync(request, cancellationToken);
    }
}