// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Errors;

namespace server.src.Persistence.Configurations.Metrics;

public class LogErrorConfiguration : IEntityTypeConfiguration<LogError>
{
    public void Configure(EntityTypeBuilder<LogError> builder)
    {
        builder.HasKey(c => c.Id);

    }
}