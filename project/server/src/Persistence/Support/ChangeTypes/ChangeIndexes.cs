// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.ChangeTypes.Models;

namespace server.src.Persistence.Support.ChangeTypes;

public class ChangeTypeIndexes : IEntityTypeConfiguration<ChangeType>
{
    public void Configure(EntityTypeBuilder<ChangeType> builder)
    {
        builder.HasIndex(ct => ct.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ChangeType)}_
                {nameof(ChangeType.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(ct 
            => new { 
                ct.Id,
                ct.Name,
                ct.IsActive
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ChangeType)}_
                {nameof(ChangeType.Id)}_
                {nameof(ChangeType.Name)}_
                {nameof(ChangeType.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}