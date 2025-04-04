// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.ChangeLogs.Models;

namespace server.src.Persistence.Support.ChangeLogs;

public class ChangeLogIndexes : IEntityTypeConfiguration<ChangeLog>
{
    public void Configure(EntityTypeBuilder<ChangeLog> builder)
    {
        builder.HasIndex(c => c.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ChangeLog)}_
                {nameof(ChangeLog.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c 
            => new { 
                c.Id,
                c.VersionLog,
                c.DateTime
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(ChangeLog)}_
                {nameof(ChangeLog.VersionLog)}_
                {nameof(ChangeLog.DateTime)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}