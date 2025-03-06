// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Persistence.Geography.Natural.Locations;

public class LocationIndexes : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasIndex(l => l.Id)
            .IsUnique()
            .HasDatabaseName($"IX_{nameof(Location)}_{nameof(Location.Id)}");

        builder.HasIndex(l 
            => new { 
                l.Id, 
                l.Longitude,
                l.Latitude,
                l.Altitude,
                l.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Location)}_
                {nameof(Location.Id)}_
                {nameof(Location.Longitude)}_
                {nameof(Location.Latitude)}_
                {nameof(Location.Altitude)}_
                {nameof(Location.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
