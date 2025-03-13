// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Countries.Models;

namespace server.src.Persistence.Geography.Administrative.Countries;

public class CountryIndexes : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasIndex(c => c.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Country)}_
                {nameof(Country.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c => c.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Country)}_
                {nameof(Country.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c 
            => new { 
                c.Id,
                c.Code,
                c.ContinentId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Country)}_
                {nameof(Country.Id)}_
                {nameof(Country.Code)}_
                {nameof(Country.ContinentId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c 
            => new { 
                c.Id, 
                c.Name,
                c.Code,
                c.Population,
                c.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Country)}_
                {nameof(Country.Id)}_
                {nameof(Country.Name)}_
                {nameof(Country.Code)}_
                {nameof(Country.Population)}_
                {nameof(Country.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
