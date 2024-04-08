using System.Net;
using MoonHotels.Connector.Application.Connection.RequestSender;
using MoonHotels.Connector.Application.Serializers;

namespace MoonHotels.Connector.Application.Connection.SupplierConnectionService;

public class ConnectionService<TSupplierRequest, TSupplierResponse> :
    IConnectionService<TSupplierRequest, TSupplierResponse>
{
    private readonly ISerializer<TSupplierRequest, TSupplierResponse> _serializer;
    private readonly IRequestSenderService<TSupplierRequest, TSupplierResponse> _supplierRequestSender;

    public ConnectionService(ISerializer<TSupplierRequest, TSupplierResponse> serializer, IRequestSenderService<TSupplierRequest, TSupplierResponse> supplierRequestSender)
    {
        _serializer = serializer;
        _supplierRequestSender = supplierRequestSender;
    }
    
    public async Task<SupplierResponseWrapper<TSupplierResponse>> SendAsync(
        SupplierRequestWrapper<TSupplierRequest> supplierRequestWrapper, 
        CancellationToken cancellationToken)
    {
        var providerResponseWrapper = new SupplierResponseWrapper<TSupplierResponse>();
        
        try
        {
            await using var requestStream =
                await _serializer.SerializeAsync(supplierRequestWrapper.Request);

            using var requestSenderResponse = await _supplierRequestSender.SendAsync(
                requestStream, supplierRequestWrapper, cancellationToken);

            if (!requestSenderResponse.Success)
            {
                providerResponseWrapper.Exception = requestSenderResponse.Exception;
                return providerResponseWrapper;
            }

            providerResponseWrapper.ResponseHeaders = requestSenderResponse.ResponseData.Headers;
            providerResponseWrapper.HttpStatusCode = requestSenderResponse.ResponseData.HttpStatusCode;

            //Only serialize ok status and integration accepted status
            if (requestSenderResponse.ResponseData.HttpStatusCode
                    is < (uint)HttpStatusCode.OK or >= (uint)HttpStatusCode.Ambiguous
                && (supplierRequestWrapper.AcceptedStatusCodes?.TryGetValue(requestSenderResponse.ResponseData
                    .HttpStatusCode, out var acceptedCode) != true || acceptedCode is < 100 or > 599))
            {
                return providerResponseWrapper;
            }
            
            try
            {
                providerResponseWrapper.Response =
                    await _serializer.DeserializeAsync(requestSenderResponse.ResponseData.ResponseStream);
            }
            //if serialization fails it's better to show this custom serialization exception
            catch (Exception e)
            {
                providerResponseWrapper.Exception = new SupplierSerializeException(e);
            }
        }
        catch (Exception e)
        {
            providerResponseWrapper.Exception = e;
        }

        return providerResponseWrapper;
    }
}