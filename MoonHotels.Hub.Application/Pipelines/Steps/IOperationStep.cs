namespace MoonHotels.Hub.Application.Pipelines;

public interface IOperationStep<THubRequest, THubResponse>
{
    Task<OperationsResponseWrapper<THubResponse>> DoOperations(
        THubRequest connectorRequest,
        CancellationToken cancellationToken = default);
}