// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Collections.Warnings.Extensions;
using server.src.Domain.Weather.Collections.Warnings.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Collections.Warnings;

public class WarningConfiguration : IEntityTypeConfiguration<Warning>
{
    private readonly string _tableName;
    private readonly string _schema;

    public WarningConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Warning> builder)
    {
        builder.ConfigureBaseEntityProperties();
        
        builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(WarningLength.NameLength); 

        builder.Property(w => w.Description)
                .IsRequired()
                .HasMaxLength(WarningLength.DescriptionLength);

        builder.Property(w => w.Code)
                .IsRequired()
                .HasMaxLength(WarningLength.CodeLength);

        builder.Property(w => w.RecommendedActions)
                .IsRequired()
                .HasMaxLength(WarningLength.RecommendedActionsLength);

        builder.Property(w => w.IsActive)
                .IsRequired();

        builder.HasMany(w => w.Forecasts)
                .WithOne(wf => wf.Warning)
                .HasForeignKey(wf => wf.WarningId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}
