// Packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Configurations.Weather;

public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ForecastConfiguration(string tableName, string schema)
    {
       _tableName = tableName;
       _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Forecast> builder)
    {
       builder.HasKey(f => f.Id);

       builder.Property(f => f.Date)
              .IsRequired();

       builder.Property(f => f.TemperatureC)
              .IsRequired();

       builder.Property(f => f.Summary)
              .HasMaxLength(200);

       builder.Property(f => f.IsRead)
              .IsRequired();

       builder.Property(f => f.Longitude)
              .IsRequired();

       builder.Property(f => f.Latitude)
              .IsRequired();

       builder.HasOne(f => f.Warning)
              .WithMany(w => w.Forecasts)
              .HasForeignKey(f => f.WarningId)
              .OnDelete(DeleteBehavior.Cascade);

       builder.ToTable(_tableName, _schema);
    }
}