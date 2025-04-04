// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.FAQCategories.Models;

namespace server.src.Persistence.Support.FAQCategories;

public class FAQCategoryIndexes: IEntityTypeConfiguration<FAQCategory>
{
    public void Configure(EntityTypeBuilder<FAQCategory> builder)
    {
        builder.HasIndex(fc => fc.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(FAQCategory)}_
                {nameof(FAQCategory.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(fq 
            => new { 
                fq.Id,
                fq.Name,
                fq.IsActive
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(FAQCategory)}_
                {nameof(FAQCategory.Id)}_
                {nameof(FAQCategory.Name)}_
                {nameof(FAQCategory.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}