using Azure;
using CsvProcessor.Controllers;
using System.Net;

namespace CsvProcessor.Models.Interfaces.Implementation;

public class FileProcessorServiceService : IFileProcessorService
{
    private readonly IApplicationFileProvider _provider;
    private readonly ILogger<FileProcessorController> _logger;
    private readonly IFileProcessorRepository _repository;

    public FileProcessorServiceService(IApplicationFileProvider provider, ILogger<FileProcessorController> logger, IFileProcessorRepository repository)
    {
        _provider = provider;
        _logger = logger;
        _repository = repository;
    }

    public async Task<Guid> Process(string fileName)
    {
        var context = _repository.StartFileProcessing(fileName);
        Task.Run(async () => { await ProcessLinks(fileName, context); });
        return context.Id;
    }

    private async Task ProcessLinks(string fileName, FileProcessorContext context)
    {
        _repository.UpdateContext(context.CopyWithNewState(FileProcessingStatus.InProgress));

        IEnumerable<string> links;
        try
        {
            links = await GetLinks(fileName);

            using (HttpClient httpClient = new HttpClient())
            {
                List<Task<LinkProcessingResult>> tasks =
                    new List<Task<LinkProcessingResult>>();

                foreach (var link in links)
                {
                    tasks.Add(FetchData(link, httpClient));
                }

                var results = await Task.WhenAll(tasks);
                _repository.UpdateContext(context.CopyWithSuccessfullState(results));
            }
        }
        catch (Exception e)
        {
            _repository.UpdateContext(context.CopyWithFailedState(e.Message));
            _logger.LogError(e, $"Error processing file {fileName}");
        }
    }

    public FileProcessorContext GetProcess(Guid fileContextId)
    {
        return _repository.GetFileProcessingContext(fileContextId);
    }

    private async Task<LinkProcessingResult> FetchData(string link, HttpClient httpClient)
    {
        try
        {
            var startTime = DateTime.Now;
            var response = await  httpClient.GetAsync(link);
            var duration  =  DateTime.Now - startTime;
            return new LinkProcessingResult() {Link = link, ResponseCode = (int)response.StatusCode, RequestTimeSeconds = duration.TotalSeconds};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new LinkProcessingResult() {Link = link, ResponseCode = (int)HttpStatusCode.BadRequest, RequestTimeSeconds = 0};
        }
    }

    private Task<IEnumerable<string>> GetLinks(string fileName)
    {
        try
        {
            return _provider.GetLinks(fileName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error processing file {fileName}");
            throw;
        }
    }
}