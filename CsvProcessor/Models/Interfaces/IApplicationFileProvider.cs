namespace CsvProcessor.Models.Interfaces;

public interface IApplicationFileProvider
{
    public Task<IEnumerable<string>> GetLinks(string file);
}