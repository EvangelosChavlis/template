// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Errors;

namespace server.src.Persistence.Configurations.Metrics;

public class LogErrorConfiguration : IEntityTypeConfiguration<LogError>
{
    private readonly string _tableName;
    private readonly string _schema;

    public LogErrorConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Error)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.StatusCode)
            .IsRequired();

        builder.Property(c => c.Instance)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.ExceptionType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.StackTrace)
            .HasMaxLength(10000);

        builder.Property(c => c.Timestamp)
            .IsRequired();

        builder.ToTable(_tableName, _schema);
    }
}
