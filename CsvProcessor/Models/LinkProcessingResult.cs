namespace CsvProcessor.Models;

public class LinkProcessingResult 
{
    public string Link { get; set; }
    public int ResponseCode { get; set; }
    
    public double RequestTimeSeconds { get; set; }
}