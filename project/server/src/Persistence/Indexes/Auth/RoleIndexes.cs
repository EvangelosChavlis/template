// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Indexes.Weather;

public class RoleIndexes : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(u => u.Id)
            .IsUnique();

        builder.HasIndex(u 
            => new { 
                u.Id, 
                u.Name, 
                u.Description
            })
            .IsUnique();
    }
}
