// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Continents.Models;

namespace server.src.Persistence.Geography.Administrative.Continents;

public class ContinentIndexes : IEntityTypeConfiguration<Continent>
{
    public void Configure(EntityTypeBuilder<Continent> builder)
    {
        builder.HasIndex(c 
            => new { 
                c.Id, 
                c.Name,
                c.Code,
                c.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Continent)}_
                {nameof(Continent.Id)}_
                {nameof(Continent.Name)}_
                {nameof(Continent.Code)}_
                {nameof(Continent.IsActive)}
            ".Replace("\n", "").Trim());
    }
}