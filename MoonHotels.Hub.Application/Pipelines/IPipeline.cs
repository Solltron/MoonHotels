namespace MoonHotels.Hub.Application.Pipelines;

public interface IPipeline<THubRequest, THubResponse>
{
    public Task<THubResponse> ProcessAsync(THubRequest request, CancellationToken cancellationToken);
}