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
        builder.HasIndex(n => n.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Neighborhood)}_
                {nameof(Neighborhood.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(n => n.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Neighborhood)}_
                {nameof(Neighborhood.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(n 
            => new { 
                n.Id,
                n.Code,
                n.DistrictId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Neighborhood)}_
                {nameof(Neighborhood.Id)}_
                {nameof(Neighborhood.Code)}_
                {nameof(Neighborhood.DistrictId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(n 
            => new { 
                n.Id, 
                n.Name,
                n.Zipcode,
                n.Code,
                n.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Neighborhood)}_
                {nameof(Neighborhood.Id)}_
                {nameof(Neighborhood.Name)}_
                {nameof(Neighborhood.Zipcode)}_
                {nameof(Neighborhood.Code)}_
                {nameof(Neighborhood.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
