// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Support.ChangeTypes.Extensions;
using server.src.Domain.Support.ChangeTypes.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Support.ChangeTypes;

public class ChangeTypeConfiguration : IEntityTypeConfiguration<ChangeType>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ChangeTypeConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<ChangeType> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(ChangeTypeSettings.NameLength);

        builder.Property(ct => ct.Description)
            .IsRequired()
            .HasMaxLength(ChangeTypeSettings.DescriptionLength);

        builder.Property(ct => ct.IsActive)
            .IsRequired();

        builder.HasMany(ct => ct.Changes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}