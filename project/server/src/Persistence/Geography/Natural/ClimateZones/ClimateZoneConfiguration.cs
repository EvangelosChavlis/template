// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;

namespace server.src.Persistence.Geography.Natural.ClimateZones;

public class ClimateZoneConfiguration : IEntityTypeConfiguration<ClimateZone>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ClimateZoneConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<ClimateZone> builder)
    {
        builder.HasKey(cz => cz.Id);

        builder.Property(cz => cz.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cz => cz.Description)
            .HasMaxLength(500);

        builder.Property(cz => cz.AvgTemperatureC)
            .IsRequired();

        builder.Property(cz => cz.AvgPrecipitationMm)
            .IsRequired();

        builder.Property(cz => cz.IsActive)
            .IsRequired();

        builder.HasMany(cz => cz.Locations)
            .WithOne(l => l.ClimateZone)
            .HasForeignKey(l => l.ClimateZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}