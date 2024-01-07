using BundlesCodingChallenge.Data;
using BundlesCodingChallenge.Models;

namespace BundlesCodingChallenge.Service;

public class BundleService
{
    private readonly BundleDbContext _dbContext;
    
    public BundleService(BundleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public int MaxBundles(Bundle bundle)
    {
        var maxBundlesCount = int.MaxValue;

        Stack<(Bundle, int)> stack = new Stack<(Bundle, int)>();
        stack.Push((bundle, 1));

        while (stack.Count > 0)
        {
            var (currentBundle, currentCount) = stack.Pop();

            // Check if the current bundle is a leaf element (spare part)
            if (currentBundle.Parts == null || currentBundle.Parts.Count == 0)
            {
                var partInventory = _dbContext.Bundles
                    .Where(b => b.Name == currentBundle.Name)
                    .Select(b => b.Count)
                    .FirstOrDefault();

                if (partInventory == 0)
                {
                    return 0;  // Part is out of stock
                }
                maxBundlesCount = Math.Min(maxBundlesCount, partInventory / currentCount);
            }
            else
            {
                // Traverse the parts of the current bundle
                foreach (var part in currentBundle.Parts)
                {
                    var partCount = part.Count * currentCount;
                    stack.Push((part, partCount));
                }
            }
        }

        return maxBundlesCount;
    }
}