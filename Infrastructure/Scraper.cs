using Microsoft.Extensions.Configuration;
using HtmlAgilityPack;

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
        HtmlWeb web = new HtmlWeb();
        //load page
        HtmlDocument document = web.Load(url);

        // get name
        var nameNode = document.DocumentNode.SelectSingleNode("//h1[contains(@class, 'listingTitle')]");
        var name = nameNode != null ? nameNode.InnerText : string.Empty;
        //Console.WriteLine("HTML Document:\n" + document.DocumentNode.OuterHtml);

        // Get property type
        var propertyTypeNode = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'property-type-label')]");
        var propertyType = propertyTypeNode != null ? propertyTypeNode.InnerText : string.Empty;

        // get bedrooms
        var bedroomNode = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'bedroom-label')]/following-sibling::span");
        var bedrooms = bedroomNode != null ? int.Parse(bedroomNode.InnerText) : 0;

        var bathroomNode = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'bathroom-label')]/following-sibling::span");
        var bathrooms = bathroomNode != null ? int.Parse(bathroomNode.InnerText) : 0;

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
        _propertyDetails.Bedrooms = bedrooms;
        _propertyDetails.Bathrooms = bathrooms;
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