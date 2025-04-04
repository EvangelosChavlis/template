// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.LogErrors.Extensions;
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Metrics.LogErrors;

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
        builder.ConfigureBaseEntityProperties();

        builder.Property(le => le.Error)
            .IsRequired()
            .HasMaxLength(LogErrorSettings.ErrorLength);

        builder.Property(le => le.StatusCode)
            .IsRequired();

        builder.Property(le => le.Instance)
            .IsRequired()
            .HasMaxLength(LogErrorSettings.InstanceLength);

        builder.Property(le => le.ExceptionType)
            .IsRequired()
            .HasMaxLength(LogErrorSettings.ExceptionTypeLength);

        builder.Property(le => le.StackTrace)
            .HasMaxLength(LogErrorSettings.StackTraceLength);

        builder.Property(le => le.Timestamp)
            .IsRequired();

        builder.HasOne(le => le.User)
            .WithMany(u => u.LogErrors)
            .HasForeignKey(le => le.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(_tableName, _schema);
    }
}
