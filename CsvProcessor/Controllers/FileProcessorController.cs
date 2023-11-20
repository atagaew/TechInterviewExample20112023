using CsvProcessor.Models;
using CsvProcessor.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CsvProcessor.Controllers;

[ApiController]
[Route("[controller]")]
public class FileProcessorController : ControllerBase
{
    private readonly ILogger<FileProcessorController> _logger;
    private readonly IFileProcessorService _processorService;

    public FileProcessorController(ILogger<FileProcessorController> logger, IFileProcessorService processorService)
    {
        _logger = logger;
        _processorService = processorService;
    }

    [HttpGet("{fileContextId}")]
    public IActionResult Get(Guid fileContextId)
    {
        var process = _processorService.GetProcess(fileContextId);
        if (process == null)
        {
            return NotFound();
        }
        return Ok(process);
    }
    [HttpPost()]
    public Task<Guid> Process([FromBody] string fileName)
    {
        return _processorService.Process(fileName);
    }
}