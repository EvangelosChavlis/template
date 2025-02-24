// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Persistence.Geography.Natural.TerrainTypes;

public class TerrainTypeConfiguration : IEntityTypeConfiguration<TerrainType>
{
    private readonly string _tableName;
    private readonly string _schema;

    public TerrainTypeConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<TerrainType> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.IsActive)
            .IsRequired();

        builder.HasMany(t => t.Locations)
            .WithOne(l => l.TerrainType)
            .HasForeignKey(l => l.TerrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}