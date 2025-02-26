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
            .IsUnique();

        builder.HasIndex(c 
            => new { 
                c.Id, 
                c.Name,
                c.IsoCode,
                c.Population,
                c.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Country)}_
                {nameof(Country.Id)}_
                {nameof(Country.Name)}_
                {nameof(Country.IsoCode)}_
                {nameof(Country.Population)}_
                {nameof(Country.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
