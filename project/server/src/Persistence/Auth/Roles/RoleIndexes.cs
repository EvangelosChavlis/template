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
