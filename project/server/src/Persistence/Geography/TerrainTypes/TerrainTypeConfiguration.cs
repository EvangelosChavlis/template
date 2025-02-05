// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.TerrainTypes.Models;

namespace server.src.Persistence.Geography.TerrainTypes;

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
        builder.HasKey(tt => tt.Id);

        builder.Property(tt => tt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(tt => tt.Description)
            .HasMaxLength(500);

        builder.Property(tt => tt.IsActive)
            .IsRequired();

        builder.HasMany(tt => tt.Locations)
            .WithOne(l => l.TerrainType)
            .HasForeignKey(l => l.TerrainTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}