namespace MoonHotels.Hub.Options;

public class ConnectorRequestConfig
{
    public int MaxParallelTransactions { get; set; }  
    
    public List<string> ConnectorEndpoints { get; set; }
}