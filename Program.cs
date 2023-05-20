//This solution presents a simple property scraper
//TODO: Add Validation for robust data handling

public class Program
{
    static void Main(string[] args)
            { 
                try
                {       
                    Console.WriteLine("Welcome to AirScraper v1.0!");
                    IScraper? scraper = null;


                    while (true)
                    {
                        Console.WriteLine("Select an option:");
                        Console.WriteLine("1. Scrape from URL");
                        Console.WriteLine("2. Scrape from file");
                        Console.WriteLine("3. Auto WebApi Scrape");
                        Console.WriteLine("4. Exit");

                        var input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                scraper = new Scraper();
                                var url = GetUserInput("Enter the URL: ");
                                //Todo validation
                                scraper.Scrape(url);
                                scraper.Display();
                                break;
                            case "2":
                                //scraper = new FileScraper();
                                //var filePath = GetUserInput("Enter the file path: ");
                                //scraper.Scrape(filePath);
                                //scraper.Display();
                                break;
                            case "3":
                                //scraper = new WebApiScraper();
                                //scraper.Scrape();
                                //scraper.Display();
                                break;                             
                            case "4":
                                Console.WriteLine("Exiting...");
                                return;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex);
                }
                
            }
        static string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
}