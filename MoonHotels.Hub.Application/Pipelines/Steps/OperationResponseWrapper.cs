using MoonHotels.Hub.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Hub.Application.Pipelines;

public class OperationsResponseWrapper<THubResponse>
{
    public THubResponse ConnectorResponse { get; init; }

    public IEnumerable<ErrorMessage> ErrorMessages { get; init; }
    
    public OperationsResponseWrapper(
        THubResponse connectorResponse)
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