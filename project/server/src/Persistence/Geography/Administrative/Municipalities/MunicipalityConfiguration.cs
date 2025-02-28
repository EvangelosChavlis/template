// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Municipalities;

public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
{
    private readonly string _tableName;
    private readonly string _schema;

    public MunicipalityConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Description)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(m => m.Population)
            .IsRequired();

        builder.Property(m => m.IsActive)
            .IsRequired();

        builder.HasOne(m => m.Region)
            .WithMany(r => r.Municipalities)
            .HasForeignKey(m => m.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Districts)
            .WithOne(d => d.Municipality)
            .HasForeignKey(d => d.MunicipalityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}