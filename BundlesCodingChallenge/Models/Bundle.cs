namespace BundlesCodingChallenge.Models;

public class Bundle
{
    public int BundleId { get; set; }
    public string? Name { get; set; }
    public int Count { get; set; }

    public List<Bundle> Parts { get; set; }
    
    public int? ParentBundleId { get; set; }
    public Bundle ParentBundle { get; set; }
}