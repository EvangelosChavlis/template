// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Persistence.Auth.Roles;

public class RoleIndexes : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.HasIndex(r 
            => new { 
                r.Id, 
                r.Name, 
                r.Description,
                r.IsActive
            })
            .IsUnique();
    }
}
