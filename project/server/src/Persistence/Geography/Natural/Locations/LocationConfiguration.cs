// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Persistence.Geography.Natural.Locations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    private readonly string _tableName;
    private readonly string _schema;

    public LocationConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.Longitude)
            .IsRequired();

        builder.Property(l => l.Latitude)
            .IsRequired();

        builder.Property(l => l.Altitude)
            .IsRequired();

        builder.Property(l => l.IsActive)
            .IsRequired();

        builder.Property(l => l.ClimateZoneId)
            .IsRequired();

        builder.Property(l => l.NaturalFeatureId)
            .IsRequired();

        builder.Property(l => l.TerrainTypeId)
            .IsRequired();

        builder.Property(l => l.TimezoneId)
            .IsRequired();

        builder.HasOne(l => l.ClimateZone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.ClimateZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.NaturalFeature)
            .WithMany(n => n.Locations)
            .HasForeignKey(l => l.NaturalFeatureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.TerrainType)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.TerrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Timezone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.TimezoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.Forecasts)
            .WithOne(f => f.Location)
            .HasForeignKey(f => f.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}