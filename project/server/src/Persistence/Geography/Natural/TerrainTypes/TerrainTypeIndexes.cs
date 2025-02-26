// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.TerrainTypes.Models;

namespace server.src.Persistence.Geography.Natural.TerrainTypes;

public class TerrainTypeIndexes : IEntityTypeConfiguration<TerrainType>
{
    public void Configure(EntityTypeBuilder<TerrainType> builder)
    {
        builder.HasIndex(t => t.Id)
            .IsUnique();

        builder.HasIndex(t 
            => new { 
                t.Id, 
                t.Name,
                t.Description,
                t.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(TerrainType)}_
                {nameof(TerrainType.Id)}_
                {nameof(TerrainType.Name)}_
                {nameof(TerrainType.Description)}_
                {nameof(TerrainType.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
