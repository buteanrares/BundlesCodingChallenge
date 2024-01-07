using BundlesCodingChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BundlesCodingChallenge.Data;

public class BundleDbContext : DbContext
{
    public DbSet<Bundle> Bundles { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "BundleDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bundle>()
            .HasKey(b => b.BundleId);

        modelBuilder.Entity<Bundle>()
            .HasMany(b => b.Parts)
            .WithOne(p => p.ParentBundle)
            .HasForeignKey(p => p.ParentBundleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}