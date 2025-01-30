// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Errors;

namespace server.src.Persistence.Indexes.Weather;

public class ErrorIndexes : IEntityTypeConfiguration<LogError>
{
    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName($"{nameof(LogError)}_{nameof(LogError.Id)}");

        builder.HasIndex(e 
            => new { 
                e.Id, 
                e.Error, 
                e.StatusCode, 
                e.Timestamp
            })
            .IsUnique()
            .HasDatabaseName($@"
                {nameof(LogError)}_
                {nameof(LogError.Id)}_
                {nameof(LogError.Error)}_
                {nameof(LogError.StatusCode)}_
                {nameof(LogError.Timestamp)}
            ".Replace("\n", "").Trim());
    }
}
