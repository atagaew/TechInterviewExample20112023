namespace CsvProcessor.Models.Interfaces;

public interface IFileProcessorService
{
    Task<Guid> Process(string fileName);
    FileProcessorContext GetProcess(Guid fileContextId);
}