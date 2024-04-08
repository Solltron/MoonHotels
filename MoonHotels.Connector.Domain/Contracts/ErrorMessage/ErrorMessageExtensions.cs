namespace MoonHotels.Connector.Domain.Contracts.ErrorMessage;

public static class ErrorMessageExtensions
{
    public static bool HaveErrors(this IEnumerable<Connector.Domain.Contracts.ErrorMessage.ErrorMessage> adviseMessages)
    {
        return adviseMessages?.Any() == true;
    }
}