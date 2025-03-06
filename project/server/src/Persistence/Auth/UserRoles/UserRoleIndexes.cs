// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.UserRoles.Models;

namespace server.src.Persistence.Auth.Roles;

public class UserRoleIndexes : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasIndex(ur 
            => new { 
                ur.Id, 
                ur.UserId, 
                ur.RoleId, 
                ur.Date
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(UserRole)}_
                {nameof(UserRole.Id)}_
                {nameof(UserRole.UserId)}_
                {nameof(UserRole.RoleId)}_
                {nameof(UserRole.Date)}
            ".Replace("\n", "").Trim());
    }
}
