namespace CsvProcessor.Models.Interfaces.Implementation;

public class LocalFileProvider : IApplicationFileProvider
{
    public async Task<IEnumerable<string>> GetLinks(string file)
    {
        using FileStream fileStream = new FileStream(file, FileMode.Open);
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(fileStream))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lines.Add(line);
            }
        }

        return lines;
    }
}