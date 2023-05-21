using Microsoft.Extensions.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

public class Scraper : IScraper
{
    private IConfiguration _configuration;
    private Property _propertyDetails;
    public Scraper()
    {
        _propertyDetails = new Property();
    }
    public void Scrape(string url)
    {
        var web = new HtmlWeb();
        //load page
        var document = web.Load(url);

        //parse json
        var scriptNode = document.DocumentNode.SelectSingleNode("//script[@id='data-deferred-state']");

       //handle bad links for now...
        if (scriptNode == null)
        {
                throw new Exception("Property Details Not Found.");
        }
        var json = scriptNode.InnerText;
        var data = JObject.Parse(json);
        var niobeMinimalClientData = data["niobeMinimalClientData"];

       //get name
       var listingTitleTokens = niobeMinimalClientData?.SelectTokens("..listingTitle");
       var firstListingTitle = listingTitleTokens?.FirstOrDefault();

       var name = firstListingTitle?.ToString() ?? string.Empty;

        // Get property type
       var listingTypeTokens = niobeMinimalClientData?.SelectTokens("..propertyType");
       var firstListingType = listingTypeTokens?.FirstOrDefault();

       var propertyType = firstListingType?.ToString() ?? string.Empty;
       
        // get bedrooms
        var descriptionItemsToken = niobeMinimalClientData?.SelectTokens("..descriptionItems");
        
        //get bathrooms


        // get amenities
        var amenitiesNodes = document.DocumentNode.SelectNodes("//div[@data-plugin-in-point-id='AMENITIES_DEFAULT']//div[@data-plugin-in-point-id='AMENITIES_DEFAULT']/div");
        List<string> amenities = new List<string>();
        if (amenitiesNodes != null)
        {
            foreach (var amenityNode in amenitiesNodes)
            {
                string amenity = amenityNode.InnerText.Trim();
                amenities.Add(amenity);
            }
        }

        //write to property      
        _propertyDetails.Name = name;
        _propertyDetails.PropertyType = propertyType;
        _propertyDetails.Bedrooms = 0;
        _propertyDetails.Bathrooms = 0;
        _propertyDetails.Amenities = amenities;
    }

    public void Display()
    {
        // Display 
        Console.WriteLine("Property Details:");
        Console.WriteLine($"Name: {_propertyDetails.Name}");
        Console.WriteLine($"Property Type: {_propertyDetails.PropertyType}");
        Console.WriteLine($"Bedrooms: {_propertyDetails.Bedrooms}");
        Console.WriteLine($"Bathrooms: {_propertyDetails.Bathrooms}");
        Console.WriteLine("Amenities:");
        foreach (var amenity in _propertyDetails.Amenities)
        {
            Console.WriteLine(amenity);
        }
    }
}