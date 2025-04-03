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
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ClimateZone)}_
                {nameof(ClimateZone.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c => c.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ClimateZone)}_
                {nameof(ClimateZone.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c 
            => new { 
                c.Id, 
                c.Name,
                c.Code,
                c.AvgTemperatureC,
                c.AvgPrecipitationMm,
                c.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(ClimateZone)}_
                {nameof(ClimateZone.Id)}_
                {nameof(ClimateZone.Name)}_
                {nameof(ClimateZone.Code)}_
                {nameof(ClimateZone.AvgTemperatureC)}_
                {nameof(ClimateZone.AvgPrecipitationMm)}_
                {nameof(ClimateZone.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
