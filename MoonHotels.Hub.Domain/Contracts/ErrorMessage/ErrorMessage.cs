namespace MoonHotels.Hub.Domain.Contracts.ErrorMessage;

public class ErrorMessage
{
    private static readonly IDictionary<ErrorMessageCode, string> _errorMessageDescriptions =
        InitErrorMessages();

    public ErrorMessageCode Code { get; init; }

    public string Description { get; init; }

    public ExternalError ExternalError { get; init; }

    private ErrorMessage()
    {
    }
    
    public ErrorMessage(ErrorMessageCode code, ExternalError externalError = default)
    {
        Code = code;
        ExternalError = externalError;

        _errorMessageDescriptions.TryGetValue(code, out var errorDescription);

        if (errorDescription is null)
        {
            throw new Exception($"Error description not found for code {code}");
        }

        Description = errorDescription;
    }

    private static IDictionary<ErrorMessageCode, string> InitErrorMessages()
    {
        return new Dictionary<ErrorMessageCode, string>
        {
            { ErrorMessageCode.Timeout, "Connection timeout with supplier" },
            { ErrorMessageCode.InternalError, "Internal error" },
            { ErrorMessageCode.SupplierError, "Supplier error, check external error for more details" },
            { ErrorMessageCode.BadRequest, "Request is not accepted." },
            { ErrorMessageCode.NoResultsFound, "No options available for the requested criteria" }
        };
    }
    
    #region ErrorBuilders
    public static ErrorMessage BuildTimeoutError()
    {
        return new ErrorMessage(ErrorMessageCode.Timeout);
    }

    public static ErrorMessage BuildInternalError()
    {
        return new ErrorMessage(ErrorMessageCode.InternalError);
    }

    public static ErrorMessage BuildInternalError(string message)
    {
        return new ErrorMessage()
        {
            Code = ErrorMessageCode.InternalError,
            Description = message
        };
    }
    
    public static ErrorMessage BuildInternalError(Exception e)
    {
        return BuildInternalError(e.ToString());
    }
    
    public static ErrorMessage BuildSupplierError(ExternalError externalError)
    {
        return new ErrorMessage(ErrorMessageCode.SupplierError, externalError);
    }

    public static ErrorMessage BuildBadRequestError()
    {
        return new ErrorMessage(ErrorMessageCode.BadRequest);
    }

    public static ErrorMessage BuildNoResultsFoundError()
    {
        return new ErrorMessage(ErrorMessageCode.NoResultsFound);
    }
    #endregion

}