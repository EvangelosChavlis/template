// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.FAQs.Models;

namespace server.src.Persistence.Support.FAQs;

public class FAQIndexes: IEntityTypeConfiguration<FAQ>
{
    public void Configure(EntityTypeBuilder<FAQ> builder)
    {
        builder.HasIndex(fc => fc.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(FAQ)}_
                {nameof(FAQ.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(fq 
            => new { 
                fq.Id,
                fq.Title,
                fq.IsActive
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(FAQ)}_
                {nameof(FAQ.Id)}_
                {nameof(FAQ.Title)}_
                {nameof(FAQ.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}