namespace CsvProcessor.Models.Interfaces;

public interface IFileProcessorRepository
{
    FileProcessorContext StartFileProcessing(string fileName);
    FileProcessorContext GetFileProcessingContext(string fileName);
    FileProcessorContext GetFileProcessingContext(Guid fileContextId);
    void UpdateContext(FileProcessorContext fileProcessorContext);
}