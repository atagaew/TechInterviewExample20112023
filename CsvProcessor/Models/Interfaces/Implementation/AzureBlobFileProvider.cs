using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace CsvProcessor.Models.Interfaces.Implementation;

public class AzureBlobFileProvider : IApplicationFileProvider
{
    private readonly IOptions<ConnectionInformation> _connectionInformation;

    public AzureBlobFileProvider(IOptions<ConnectionInformation> connectionInformation)
    {
        _connectionInformation = connectionInformation;
    }

    public async Task<IEnumerable<string>> GetLinks(string file)
    {
        var blobServiceClient = new BlobServiceClient(_connectionInformation.Value.ConnectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(_connectionInformation.Value.ContainerName);
        var blobClient = blobContainerClient.GetBlobClient(_connectionInformation.Value.BlobName);

        BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(blobDownloadInfo.Content))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lines.Add(line);
            }
        }

        return lines;
    }
}