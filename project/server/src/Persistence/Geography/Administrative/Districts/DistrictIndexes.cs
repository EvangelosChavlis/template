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
        builder.HasIndex(d => d.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(District)}_
                {nameof(District.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(d => d.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(District)}_
                {nameof(District.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(d 
            => new { 
                d.Id,
                d.Code,
                d.MunicipalityId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(District)}_
                {nameof(District.Id)}_
                {nameof(District.Code)}_
                {nameof(District.MunicipalityId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(d 
            => new { 
                d.Id, 
                d.Name,
                d.Population,
                d.Code,
                d.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(District)}_
                {nameof(District.Id)}_
                {nameof(District.Name)}_
                {nameof(District.Population)}_
                {nameof(District.Code)}_
                {nameof(District.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
