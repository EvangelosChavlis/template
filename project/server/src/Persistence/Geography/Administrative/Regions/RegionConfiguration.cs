// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Regions.Extensions;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Regions;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    private readonly string _tableName;
    private readonly string _schema;

    public RegionConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(RegionSettings.NameLength);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(RegionSettings.DescriptionLength);

        builder.Property(r => r.AreaKm2)
            .IsRequired();

        builder.Property(r => r.Code)
            .IsRequired()
            .HasMaxLength(RegionSettings.CodeLength);

        builder.Property(r => r.IsActive)
            .IsRequired();

        builder.HasOne(r => r.State)
            .WithMany(s => s.Regions)
            .HasForeignKey(r => r.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.Municipalities)
            .WithOne(m => m.Region)
            .HasForeignKey(m => m.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
