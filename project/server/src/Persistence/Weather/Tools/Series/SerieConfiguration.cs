// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.Series.Extensions;
using server.src.Domain.Weather.Tools.Series.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Tools.Series;

public class SerieConfiguration : IEntityTypeConfiguration<Serie>
{
    private readonly string _tableName;
    private readonly string _schema;

    public SerieConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Serie> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(s => s.Timestamp)
            .IsRequired();

        builder.Property(s => s.Value)
            .IsRequired();

        builder.Property(s => s.Remarks)
            .IsRequired()
            .HasMaxLength(SerieSettings.RemarksLength);

        builder.HasOne(s => s.Sensor)
            .WithMany(se => se.Series)
            .HasForeignKey(s => s.SensorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}