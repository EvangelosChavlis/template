// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Configurations.Weather;

public class WarningConfiguration : IEntityTypeConfiguration<Warning>
{
    public void Configure(EntityTypeBuilder<Warning> builder)
    {
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100); 

        builder.Property(w => w.Description)
                .HasMaxLength(500);

        builder.HasMany(w => w.Forecasts)
                .WithOne(wf => wf.Warning)
                .HasForeignKey(wf => wf.WarningId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Warnings");
    }
}
