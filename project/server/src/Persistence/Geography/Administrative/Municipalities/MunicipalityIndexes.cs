// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Municipalities.Models;

namespace server.src.Persistence.Geography.Administrative.Municipalities;

public class MunicipalityIndexes : IEntityTypeConfiguration<Municipality>
{
    public void Configure(EntityTypeBuilder<Municipality> builder)
    {
        builder.HasIndex(m => m.Id)
            .IsUnique();

        builder.HasIndex(m 
            => new { 
                m.Id, 
                m.Name,
                m.Population,
                m.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Municipality)}_
                {nameof(Municipality.Id)}_
                {nameof(Municipality.Name)}_
                {nameof(Municipality.Population)}_
                {nameof(Municipality.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
