// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;

namespace server.src.Persistence.Geography.Natural.NaturalFeatures;

public class NaturalFeatureIndexes : IEntityTypeConfiguration<NaturalFeature>
{
    public void Configure(EntityTypeBuilder<NaturalFeature> builder)
    {
        builder.HasIndex(nf => nf.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(NaturalFeature)}_
                {nameof(NaturalFeature.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(nf 
            => new { 
                nf.Id, 
                nf.Name,
                nf.Code,
                nf.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(NaturalFeature)}_
                {nameof(NaturalFeature.Id)}_
                {nameof(NaturalFeature.Name)}_
                {nameof(NaturalFeature.Code)}_
                {nameof(NaturalFeature.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
