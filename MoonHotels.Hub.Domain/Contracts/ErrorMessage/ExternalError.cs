namespace MoonHotels.Hub.Domain.Contracts.ErrorMessage;

public class ExternalError
{
    private ExternalError()
    {
    }

    public ExternalError(string code, string message, int httpStatusCode = 200)
    {
        Code = code;
        Message = message;
        HttpStatusCode = httpStatusCode;
    }

    public string Code { get; init; }

    public string Message { get; init; }

    public int HttpStatusCode { get; init; }
}