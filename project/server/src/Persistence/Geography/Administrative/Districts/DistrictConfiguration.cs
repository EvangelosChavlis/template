// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Districts.Extensions;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Districts;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    private readonly string _tableName;
    private readonly string _schema;

    public DistrictConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(DistrictSettings.NameLength);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(DistrictSettings.DescriptionLength);

        builder.Property(d => d.Population)
            .IsRequired();

        builder.Property(d => d.AreaKm2)
            .IsRequired();

        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(DistrictSettings.CodeLength);

        builder.Property(d => d.IsActive)
            .IsRequired();

        builder.HasOne(d => d.Municipality)
            .WithMany(m => m.Districts)
            .HasForeignKey(d => d.MunicipalityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Neighborhoods)
            .WithOne(n => n.District)
            .HasForeignKey(n => n.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}