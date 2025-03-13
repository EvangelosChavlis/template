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
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Role)}_
                {nameof(Role.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(r 
            => new { 
                r.Id, 
                r.Name, 
                r.Description,
                r.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Role)}_
                {nameof(Role.Id)}_
                {nameof(Role.Name)}_
                {nameof(Role.Description)}_
                {nameof(Role.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
