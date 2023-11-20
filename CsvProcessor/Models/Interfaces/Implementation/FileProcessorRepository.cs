namespace CsvProcessor.Models.Interfaces.Implementation;

public class FileProcessorRepository : IFileProcessorRepository
{
    private readonly Dictionary<string, FileProcessorContext> _fileProcessorContexts;
    public FileProcessorRepository()
    {
        _fileProcessorContexts = new Dictionary<string, FileProcessorContext>();
    }

    public FileProcessorContext StartFileProcessing(string fileName)
    {
        var context = new FileProcessorContext(fileName);

        if (_fileProcessorContexts.ContainsKey(fileName))
        {
            var existingContext = _fileProcessorContexts[fileName];
            if (existingContext.Status == FileProcessingStatus.InProgress)
            {
                throw new Exception("File is processing");
            }
            _fileProcessorContexts[fileName] = context;
        }
        else
        {
            _fileProcessorContexts.Add(fileName, context);
        }

        return context;
    }

    public FileProcessorContext GetFileProcessingContext(string fileName)
    {
        return _fileProcessorContexts[fileName];
    }

    public FileProcessorContext GetFileProcessingContext(Guid fileContextId)
    {
        return _fileProcessorContexts.Values.FirstOrDefault(x => x.Id == fileContextId);
    }

    public void UpdateContext(FileProcessorContext context)
    {
        _fileProcessorContexts[context.FileName] = context;
    }
}