// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Extensions;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Natural.SurfaceTypes;

public class SurfaceTypeConfiguration : IEntityTypeConfiguration<SurfaceType>
{
    private readonly string _tableName;
    private readonly string _schema;

    public SurfaceTypeConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<SurfaceType> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(SurfaceTypeSettings.NameLength);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(SurfaceTypeSettings.DescriptionLength);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(SurfaceTypeSettings.CodeLength);

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasMany(s => s.Locations)
            .WithOne(l => l.SurfaceType)
            .HasForeignKey(l => l.SurfaceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}