// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.ChangeLogs.Extensions;
using server.src.Domain.Support.ChangeLogs.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Support.ChangeLogs;

public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ChangeLogConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<ChangeLog> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(c => c.VersionLog)
            .IsRequired()
            .HasMaxLength(ChangeLogSettings.VersionLogLength);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(ChangeLogSettings.NameLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(ChangeLogSettings.DescriptionLength);

        builder.Property(c => c.DateTime)
            .IsRequired();

        builder.HasMany(c => c.Changes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}