// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.NaturalFeatures.Extensions;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Natural.NaturalFeatures;

public class NaturalFeatureConfiguration : IEntityTypeConfiguration<NaturalFeature>
{
    private readonly string _tableName;
    private readonly string _schema;

    public NaturalFeatureConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<NaturalFeature> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(nf => nf.Name)
            .IsRequired()
            .HasMaxLength(NaturalFeatureLength.NameLength);

        builder.Property(nf => nf.Description)
            .IsRequired()
            .HasMaxLength(NaturalFeatureLength.DescriptionLength);

        builder.Property(nf => nf.Code)
            .IsRequired()
            .HasMaxLength(NaturalFeatureLength.CodeLength);

        builder.Property(nf => nf.IsActive)
            .IsRequired();

        builder.HasMany(nf => nf.Locations)
            .WithOne(l => l.NaturalFeature)
            .HasForeignKey(l => l.NaturalFeatureId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}