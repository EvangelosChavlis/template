// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.LogErrors.Models;

namespace server.src.Persistence.Metrics.LogErrors;

public class LogErrorIndexes : IEntityTypeConfiguration<LogError>
{
    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.HasIndex(le => le.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(LogError)}_
                {nameof(LogError.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(le 
            => new { 
                le.Id, 
                le.Error, 
                le.StatusCode, 
                le.Timestamp
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(LogError)}_
                {nameof(LogError.Id)}_
                {nameof(LogError.Error)}_
                {nameof(LogError.StatusCode)}_
                {nameof(LogError.Timestamp)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
