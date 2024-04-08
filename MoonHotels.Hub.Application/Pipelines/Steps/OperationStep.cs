using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using MoonHotels.Hub.Application.Connection;
using MoonHotels.Hub.Application.Operations;
using MoonHotels.Hub.Domain.Contracts.ErrorMessage;

namespace MoonHotels.Hub.Application.Pipelines.Steps;

public class OperationStep<THubRequest, THubResponse, TSupplierRequest, TSupplierResponse> : IOperationStep<THubRequest, THubResponse>
{
    private readonly
        IHubOperation<THubRequest, THubResponse, TSupplierRequest,
            TSupplierResponse> _operation;


    private readonly ISupplierConnectionService<TSupplierRequest, TSupplierResponse> _supplierConnectionService;

    public OperationStep(IServiceProvider serviceProvider)
    {
        _operation = serviceProvider.GetRequiredService<IHubOperation<THubRequest, THubResponse, TSupplierRequest,
            TSupplierResponse>>();
        _supplierConnectionService = serviceProvider
            .GetRequiredService<ISupplierConnectionService<TSupplierRequest, TSupplierResponse>>();
    }
    
    public async Task<OperationsResponseWrapper<THubResponse>> DoOperations(
        THubRequest connectorRequest, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!_operation.TryValidateRequest(connectorRequest, out var errorMessages))
            {
                return new OperationsResponseWrapper<THubResponse>(errorMessages);
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
                    var response = await _supplierConnectionService.SendAsync(requestWrapper,
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

            /*if (!_supplierConnectionValidation.IsValid(requestWrappers, responseWrappers, responseWrappers.Length,
                    out var validateConnectionAdviseMessages))
            {
                return new OperationsResponseWrapper<TConnectorResponse>(validateConnectionAdviseMessages);
            } */

            if (!_operation.TryValidateResponse(responseWrappers.Select(response => response.Response),
                    out var validateResponseAdviseMessages))
            {
                if (validateResponseAdviseMessages?.Any() != true)
                {
                    validateResponseAdviseMessages = new[] {
                        ErrorMessage.BuildInternalError("Validation failed: " +
                        "TryValidateSupplierResponses returned false without providing an AdviseMessage.") };
                };

                return new OperationsResponseWrapper<THubResponse>(validateResponseAdviseMessages);
            }

            var response =
                _operation.ParseResponses(connectorRequest, responseWrappers);
            
            return new OperationsResponseWrapper<THubResponse>(response);
        }
        catch (Exception e)
        {
            return new OperationsResponseWrapper<THubResponse>(ErrorMessage.BuildInternalError(e));
        }
    }
}