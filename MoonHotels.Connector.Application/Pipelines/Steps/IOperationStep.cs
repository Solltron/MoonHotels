namespace MoonHotels.Connector.Application.Pipelines.Steps;

public interface IOperationStep<in TConnectorRequest, TConnectorResponse>
{
    Task<OperationsResponseWrapper<TConnectorResponse>> DoOperations(
        TConnectorRequest connectorRequest,
        CancellationToken cancellationToken = default);
}