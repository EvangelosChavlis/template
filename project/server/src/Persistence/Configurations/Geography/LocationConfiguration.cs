// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Geography;

namespace server.src.Persistence.Configurations.Geography;

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

        builder.Property(l => l.TimeZoneId)
            .IsRequired();

        builder.Property(l => l.TerrainTypeId)
            .IsRequired();

        builder.Property(l => l.ClimateZoneId)
            .IsRequired();

        builder.HasOne(l => l.TimeZone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.TimeZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.TerrainType)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.TerrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.ClimateZone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.ClimateZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.Forecasts)
            .WithOne(f => f.Location)
            .HasForeignKey(f => f.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}