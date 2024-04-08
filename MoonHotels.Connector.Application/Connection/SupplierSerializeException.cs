namespace MoonHotels.Connector.Application.Connection;

public class SupplierSerializeException : Exception
{
    public SupplierSerializeException(Exception innerException) : base("Unable to serialize response", innerException)
    {
    }
}