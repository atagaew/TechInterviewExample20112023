namespace CsvProcessor.Models;

public class FileProcessorContext
{
    public FileProcessorContext(string fileName)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        Results = Array.Empty<LinkProcessingResult>();
    }

    private FileProcessorContext()
    {
    }

    public Guid Id { get; private init; }

    public string FileName { get; private init; }

    public FileProcessingStatus Status { get; private init; }

    public LinkProcessingResult[] Results { get; private init; }

    public string ErrorReason { get; private init; }

    public FileProcessorContext CopyWithNewState(FileProcessingStatus state)
    {
        return new FileProcessorContext() { Id = Id, FileName = FileName, Results = Results, Status = state };
    }
    public FileProcessorContext CopyWithFailedState(string reason)
    {
        return new FileProcessorContext() { Id = Id, FileName = FileName, Results = Results, Status = FileProcessingStatus.Failed, ErrorReason = reason };
    }

    public FileProcessorContext CopyWithSuccessfullState(LinkProcessingResult[] results)
    {
        return new FileProcessorContext() { Id = Id, FileName = FileName, Results = results, Status = FileProcessingStatus.Completed };
    }
}