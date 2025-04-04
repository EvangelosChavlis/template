// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.Changes.Extensions;
using server.src.Domain.Support.Changes.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Support.Changes;

public class ChangeConfiguration : IEntityTypeConfiguration<Change>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ChangeConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Change> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(ChangeSettings.NameLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(ChangeSettings.DescriptionLength);

        builder.HasOne(c => c.ChangeLog)
            .WithMany(cl => cl.Changes)
            .HasForeignKey(c => c.ChangeLogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.ChangeType)
            .WithMany(ct => ct.Changes)
            .HasForeignKey(c => c.ChangeTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}