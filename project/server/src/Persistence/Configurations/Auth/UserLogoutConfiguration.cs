// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Configurations.Auth;

public class UserLogoutConfiguration : IEntityTypeConfiguration<UserLogout>
{
    private readonly string _tableName;
    private readonly string _schema;

    public UserLogoutConfiguration(string tableName, string schema)
    {
       _tableName = tableName;
       _schema = schema;
    }

    public void Configure(EntityTypeBuilder<UserLogout> builder)
   {
       builder.HasKey(ul => ul.Id);

       builder.Property(ul => ul.Date)
              .IsRequired();

       builder.HasOne(ul => ul.User)
              .WithMany(u => u.UserLogouts)
              .HasForeignKey(ul => ul.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

       builder.Property(ul => ul.LoginProvider)
              .IsRequired()
              .HasMaxLength(100);

       builder.Property(ul => ul.ProviderKey)
              .IsRequired()
              .HasMaxLength(100);

       builder.ToTable(_tableName, _schema);
    }
}