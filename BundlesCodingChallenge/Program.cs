using BundlesCodingChallenge.Data;
using BundlesCodingChallenge.Models;
using BundlesCodingChallenge.Service;

internal class Program
{
    private static void Main()
    {
        var dbContext = new BundleDbContext();

        // Inventory of parts
        var inventory = new List<Bundle>
        {
            new() { Name = "Seat", Count = 50 },
            new() { Name = "Pedal", Count = 60 },
            new() { Name = "Frame", Count = 60 },
            new() { Name = "Tube", Count = 35 }
        };
        
        // Top-level bundle structure
        var bundle = new Bundle
        {
            Name = "MainBundle",
            Count = 1,
            Parts =
            [
                new Bundle { Name = "Seat", Count = 1 },
                new Bundle { Name = "Pedal", Count = 2 },
                new Bundle
                {
                    Name = "Wheel",
                    Count = 2,
                    Parts =
                    [
                        new Bundle { Name = "Frame", Count = 1 },
                        new Bundle { Name = "Tube", Count = 1 }
                    ]
                }
            ]
        };

        dbContext.Bundles.AddRange(inventory);
        dbContext.SaveChanges();

        var bundleService = new BundleService(dbContext);
        
        var result = bundleService.MaxBundles(bundle);
        Console.WriteLine("Maximum number of bundles that can be built: " + result);
    }
}