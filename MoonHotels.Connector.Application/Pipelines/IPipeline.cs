namespace MoonHotels.Connector.Application.Pipelines;

public interface IPipeline<in TConnectorRequest, TConnectorResponse>
{
    public Task<TConnectorResponse> ProcessAsync(TConnectorRequest request, CancellationToken cancellationToken);
}