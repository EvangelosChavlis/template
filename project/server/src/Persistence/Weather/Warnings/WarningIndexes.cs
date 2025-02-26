// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Persistence.Weather.Warnings;

public class WarningIndexes : IEntityTypeConfiguration<Warning>
{
    public void Configure(EntityTypeBuilder<Warning> builder)
    {
        builder.HasIndex(w => w.Id)
            .IsUnique();

        builder.HasIndex(w 
            => new { 
                w.Id, 
                w.Name, 
                w.Description 
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Warning)}_
                {nameof(Warning.Id)}_
                {nameof(Warning.Name)}_
                {nameof(Warning.Description)}
            ".Replace("\n", "").Trim());
    }
}