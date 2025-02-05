// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Persistence.Auth.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly string _tableName;
    private readonly string _schema;
    
    public RoleConfiguration(string tableName, string schema)
    {
       _tableName = tableName;
       _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
       builder.HasKey(r => r.Id);

       builder.Property(r => r.Name)
              .IsRequired()
              .HasMaxLength(100);

       builder.Property(r => r.NormalizedName)
              .IsRequired()
              .HasMaxLength(100);

       builder.Property(r => r.Description)
              .IsRequired()
              .HasMaxLength(250);

       builder.Property(r => r.IsActive)
              .IsRequired();

       builder.Property(r => r.Version)
              .IsRequired();

       builder.HasMany(r => r.UserRoles)
              .WithOne(ur => ur.Role)
              .HasForeignKey(ur => ur.RoleId)
              .IsRequired();

       builder.ToTable(_tableName, _schema);
    }
}