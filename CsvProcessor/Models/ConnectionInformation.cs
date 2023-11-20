namespace CsvProcessor.Models;

public class ConnectionInformation
{
    public string ConnectionString { get; set; }
    public string ContainerName { get; set; }
    
    public string BlobName { get; set; }
}