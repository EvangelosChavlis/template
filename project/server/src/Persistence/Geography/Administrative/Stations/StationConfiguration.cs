// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Stations.Extensions;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Stations;

public class StationConfiguration : IEntityTypeConfiguration<Station>
{
    private readonly string _tableName;
    private readonly string _schema;

    public StationConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(StationLength.NameLength);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(StationLength.DescriptionLength);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(StationLength.CodeLength);

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.Property(s => s.LocationId)
            .IsRequired();

        builder.Property(s => s.NeighborhoodId)
            .IsRequired(false);

        builder.HasOne(s => s.Location)
            .WithOne()
            .HasForeignKey<Station>(s => s.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Neighborhood)
            .WithOne(n => n.Station)
            .HasForeignKey<Station>(s => s.NeighborhoodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(s => s.Forecasts)
            .WithOne(f => f.Station)
            .HasForeignKey(f => f.StationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}