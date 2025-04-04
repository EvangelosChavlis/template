// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.Changes.Models;

namespace server.src.Persistence.Support.Changes;

public class ChangeIndexes : IEntityTypeConfiguration<Change>
{
    public void Configure(EntityTypeBuilder<Change> builder)
    {
        builder.HasIndex(c => c.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Change)}_
                {nameof(Change.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c 
            => new { 
                c.Id,
                c.Name
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Change)}_
                {nameof(Change.Id)}_
                {nameof(Change.Name)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}