using MoonHotels.Connector.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Connector.Application.Pipelines.Steps;

public class OperationsResponseWrapper<TConnectorResponse>
{
    public TConnectorResponse ConnectorResponse { get; init; }

    public IEnumerable<ErrorMessage> ErrorMessages { get; init; }
    
    public OperationsResponseWrapper(
        TConnectorResponse connectorResponse)
    {
        ConnectorResponse = connectorResponse;
    }

    public OperationsResponseWrapper(
        ErrorMessage errorMessage)
    {
        ErrorMessages = new[] { errorMessage };
    }

    public OperationsResponseWrapper(
        IEnumerable<ErrorMessage> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}