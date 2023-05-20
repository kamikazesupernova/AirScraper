using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public class FileScraper : IScraper
{
    private readonly IConfiguration _configuration;
    private List<string> propertyDetails;

    public FileScraper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Scrape(string input)
    {
        //var filePath = _configuration.GetValue<string>("FilePath");
        propertyDetails = ReadPropertyDetailsFromFile(input);
    }

    public void Display()
    {
        foreach (var detail in propertyDetails)
        {
            Console.WriteLine(detail);
        }
    }

    private List<string> ReadPropertyDetailsFromFile(string filePath)
    {
        List<string> details = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                details.Add(line);
            }
        }

        return details;
    }
}