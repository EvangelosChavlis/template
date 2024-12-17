// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// packages
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Configurations.Weather
{
    public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
    {
        public void Configure(EntityTypeBuilder<Forecast> builder)
        {
            // Configure the primary key
            builder.HasKey(wf => wf.Id);

            // Configure the required fields
            builder.Property(wf => wf.Date)
                   .IsRequired();

            builder.Property(wf => wf.TemperatureC)
                   .IsRequired();

            builder.Property(wf => wf.Summary)
                   .HasMaxLength(200);

            // Configure the relationship with Alert (many-to-one)
            builder.HasOne(wf => wf.Warning)
                   .WithMany(a => a.Forecasts)
                   .HasForeignKey(wf => wf.WarningId)
                   .OnDelete(DeleteBehavior.Cascade); // Optional: specify behavior on delete
        }
    }
}
