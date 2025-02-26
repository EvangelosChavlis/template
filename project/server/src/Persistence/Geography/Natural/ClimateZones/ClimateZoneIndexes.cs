// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Persistence.Geography.Natural.ClimateZones;

public class ClimateZoneIndexes : IEntityTypeConfiguration<ClimateZone>
{
    public void Configure(EntityTypeBuilder<ClimateZone> builder)
    {
        builder.HasIndex(c => c.Id)
            .IsUnique();

        builder.HasIndex(c 
            => new { 
                c.Id, 
                c.Name,
                c.AvgTemperatureC,
                c.AvgPrecipitationMm,
                c.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(ClimateZone)}_
                {nameof(ClimateZone.Id)}_
                {nameof(ClimateZone.Name)}_
                {nameof(ClimateZone.AvgTemperatureC)}_
                {nameof(ClimateZone.AvgPrecipitationMm)}_
                {nameof(ClimateZone.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
