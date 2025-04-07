// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Neighborhoods.Extensions;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Neighborhoods;

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    private readonly string _tableName;
    private readonly string _schema;

    public NeighborhoodConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(NeighborhoodSettings.NameLength);

        builder.Property(n => n.Description)
            .IsRequired()
            .HasMaxLength(NeighborhoodSettings.DescriptionLength);

        builder.Property(n => n.Population)
            .IsRequired();

        builder.Property(n => n.AreaKm2)
            .IsRequired();

        builder.Property(n => n.Zipcode)
            .IsRequired()
            .HasMaxLength(NeighborhoodSettings.ZipCodeLength);

        builder.Property(n => n.Code)
            .IsRequired()
            .HasMaxLength(NeighborhoodSettings.CodeLength);

        builder.Property(n => n.IsActive)
            .IsRequired();

        builder.HasOne(n => n.District)
            .WithMany(d => d.Neighborhoods)
            .HasForeignKey(n => n.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(n => n.Users)
            .WithOne(u => u.Neighborhood)
            .HasForeignKey(u => u.NeighborhoodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}