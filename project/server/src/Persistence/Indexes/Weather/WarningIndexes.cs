// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Indexes.Weather;

public class WarningIndexes : IEntityTypeConfiguration<Warning>
{
    public void Configure(EntityTypeBuilder<Warning> builder)
    {
        builder.HasIndex(w => w.Id)
            .IsUnique();

        builder.HasIndex(w 
            => new { 
                w.Id, 
                w.Name, 
                w.Description 
            })
            .IsUnique();
    }
}
