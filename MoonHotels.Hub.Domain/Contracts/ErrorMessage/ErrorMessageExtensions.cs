namespace MoonHotels.Hub.Domain.Contracts.ErrorMessage;

public static class ErrorMessageExtensions
{
    public static bool HaveErrors(this IEnumerable<ErrorMessage> adviseMessages)
    {
        return adviseMessages?.Any() == true;
    }
}