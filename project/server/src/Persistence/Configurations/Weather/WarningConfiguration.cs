// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Configurations.Weather;

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
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100); 

        builder.Property(w => w.Description)
                .HasMaxLength(500);

        builder.Property(w => w.RecommendedActions)
                .HasMaxLength(250);

        builder.Property(w => w.IsActive)
                .IsRequired();

        builder.HasMany(w => w.Forecasts)
                .WithOne(wf => wf.Warning)
                .HasForeignKey(wf => wf.WarningId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}
