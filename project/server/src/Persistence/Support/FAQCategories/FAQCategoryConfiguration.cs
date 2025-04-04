// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.FAQCategories.Extensions;
using server.src.Domain.Support.FAQCategories.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Support.FAQCategories;

public class FAQCategoryConfiguration : IEntityTypeConfiguration<FAQCategory>
{
    private readonly string _tableName;
    private readonly string _schema;

    public FAQCategoryConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<FAQCategory> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(fc => fc.Name)
            .IsRequired()
            .HasMaxLength(FAQCategorySettings.NameLength);

        builder.Property(fc => fc.Description)
            .IsRequired()
            .HasMaxLength(FAQCategorySettings.DescriptionLength);

        builder.Property(fc => fc.IsActive)
            .IsRequired();

        builder.HasMany(fc => fc.FAQs)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}