// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Regions.Models;

namespace server.src.Persistence.Geography.Administrative.Regions;

public class RegionIndexes : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.HasIndex(r 
            => new { 
                r.Id, 
                r.Name,
                r.AreaKm2,
                r.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Region)}_
                {nameof(Region.Id)}_
                {nameof(Region.Name)}_
                {nameof(Region.AreaKm2)}_
                {nameof(Region.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
