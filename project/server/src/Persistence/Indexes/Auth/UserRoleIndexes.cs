// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Indexes.Auth;

public class UserRoleIndexes : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasIndex(ur => ur.Id)
            .IsUnique();

        builder.HasIndex(ur
            => new { 
                ur.Id, 
                ur.UserId, 
                ur.RoleId, 
                ur.Date
            })
            .IsUnique();
    }
}
