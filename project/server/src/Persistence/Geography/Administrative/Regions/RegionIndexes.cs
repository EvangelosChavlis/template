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
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Region)}_
                {nameof(Region.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(r => r.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Region)}_
                {nameof(Region.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(r 
            => new { 
                r.Id,
                r.Code,
                r.StateId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Region)}_
                {nameof(Region.Id)}_
                {nameof(Region.Code)}_
                {nameof(Region.StateId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(r 
            => new { 
                r.Id, 
                r.Name,
                r.AreaKm2,
                r.Code,
                r.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Region)}_
                {nameof(Region.Id)}_
                {nameof(Region.Name)}_
                {nameof(Region.AreaKm2)}_
                {nameof(Region.Code)}_
                {nameof(Region.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
