// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Districts.Models;

namespace server.src.Persistence.Geography.Administrative.Districts;

public class DistrictIndexes : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasIndex(d 
            => new { 
                d.Id, 
                d.Name,
                d.Population,
                d.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(District)}_
                {nameof(District.Id)}_
                {nameof(District.Name)}_
                {nameof(District.Population)}_
                {nameof(District.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
