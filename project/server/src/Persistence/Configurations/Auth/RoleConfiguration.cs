// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;
using server.src.Persistence.Extensions;

namespace server.src.Persistence.Configurations.Auth;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly string _tableName;
    private readonly string _schema;
    
    public RoleConfiguration(string tableName, string schema)
    {
       _tableName = PluralizedName.GetPluralizedName<Role>();
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