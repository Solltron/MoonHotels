using System.Collections.Concurrent;
using MoonHotels.Connector.Application.Connection;
using MoonHotels.Connector.Application.Connection.SupplierConnectionService;
using MoonHotels.Connector.Application.Operations;
using MoonHotels.Connector.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Connector.Application.Pipelines.Steps;

public class OperationStep<TConnectorRequest, TConnectorResponse, TSupplierRequest, TSupplierResponse> : IOperationStep<TConnectorRequest, TConnectorResponse>
{
    private readonly
        IConnectorOperation<TConnectorRequest, TConnectorResponse, TSupplierRequest,
            TSupplierResponse> _operation;


    private readonly IConnectionService<TSupplierRequest, TSupplierResponse> _connectionService;

    public OperationStep(IServiceProvider serviceProvider)
    {
        _operation = serviceProvider.GetRequiredService<IConnectorOperation<TConnectorRequest, TConnectorResponse, TSupplierRequest,
            TSupplierResponse>>();
        _connectionService = serviceProvider
            .GetRequiredService<IConnectionService<TSupplierRequest, TSupplierResponse>>();
    }
    
    public async Task<OperationsResponseWrapper<TConnectorResponse>> DoOperations(
        TConnectorRequest connectorRequest, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!_operation.TryValidateRequest(connectorRequest, out var errorMessages))
            {
                return new OperationsResponseWrapper<TConnectorResponse>(errorMessages);
            }
            
            var requestWrappers =
                _operation.BuildRequests(connectorRequest).ToArray();
            
            ConcurrentBag<SupplierResponseWrapper<TSupplierResponse>> supplierResponseWrappers = new();
            
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 5
            };
            
            await Parallel.ForEachAsync(requestWrappers, parallelOptions, async (requestWrapper, _) =>
            {
                try
                {
                    var response = await _connectionService.SendAsync(requestWrapper,
                        cancellationToken);

                    supplierResponseWrappers.Add(response);
                }
                catch (Exception ex)
                {
                    supplierResponseWrappers.Add(new SupplierResponseWrapper<TSupplierResponse>()
                    {
                        Exception = ex,
                        RequestId = requestWrapper.RequestId
                    });
                }
            });

            var responseWrappers = supplierResponseWrappers.ToArray();

            if (!_operation.TryValidateResponse(responseWrappers.Select(response => response.Response),
                    out var validateResponseAdviseMessages))
            {
                if (validateResponseAdviseMessages?.Any() != true)
                {
                    validateResponseAdviseMessages = new[] {
                        ErrorMessage.BuildInternalError("Validation failed: " +
                        "TryValidateSupplierResponses returned false without providing an AdviseMessage.") };
                };

                return new OperationsResponseWrapper<TConnectorResponse>(validateResponseAdviseMessages);
            }

            var response =
                _operation.ParseResponses(connectorRequest, responseWrappers);
            
            return new OperationsResponseWrapper<TConnectorResponse>(response);
        }
        catch (Exception e)
        {
            return new OperationsResponseWrapper<TConnectorResponse>(ErrorMessage.BuildInternalError(e));
        }
    }
}