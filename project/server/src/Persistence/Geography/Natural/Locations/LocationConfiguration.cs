// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.src.Domain.Geography.Administrative.Stations.Models;


// source
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Configuration;

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
        builder.ConfigureBaseEntityProperties();

        builder.Property(l => l.Longitude)
            .IsRequired();

        builder.Property(l => l.Latitude)
            .IsRequired();

        builder.Property(l => l.Altitude)
            .IsRequired();

        builder.Property(l => l.Depth)
            .IsRequired();

        builder.Property(l => l.IsActive)
            .IsRequired();

        builder.Property(l => l.ClimateZoneId)
            .IsRequired();

        builder.Property(l => l.NaturalFeatureId)
            .IsRequired();

        builder.Property(l => l.SurfaceTypeId)
            .IsRequired();

        builder.Property(l => l.TimezoneId)
            .IsRequired();

        builder.Property(l => l.StationId)
            .IsRequired(false);

        builder.HasOne(l => l.ClimateZone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.ClimateZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.NaturalFeature)
            .WithMany(n => n.Locations)
            .HasForeignKey(l => l.NaturalFeatureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.SurfaceType)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.SurfaceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Timezone)
            .WithMany(tz => tz.Locations)
            .HasForeignKey(l => l.TimezoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Station)
            .WithOne(s => s.Location)
            .HasForeignKey<Station>(s => s.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}