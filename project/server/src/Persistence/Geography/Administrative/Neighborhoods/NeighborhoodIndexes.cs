// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Persistence.Geography.Administrative.Neighborhoods;

public class NeighborhoodIndexes : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.HasIndex(n 
            => new { 
                n.Id, 
                n.Name,
                n.Zipcode,
                n.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Neighborhood)}_
                {nameof(Neighborhood.Id)}_
                {nameof(Neighborhood.Name)}_
                {nameof(Neighborhood.Zipcode)}_
                {nameof(Neighborhood.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
