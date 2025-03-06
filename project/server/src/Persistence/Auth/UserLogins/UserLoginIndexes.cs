// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.UserLogins.Models;

namespace server.src.Persistence.Auth.UserLogins;

public class UserLoginIndexes : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.HasIndex(ul 
            => new { 
                ul.Id, 
                ul.LoginProvider, 
                ul.ProviderDisplayName, 
                ul.Date
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(UserLogin)}_
                {nameof(UserLogin.Id)}_
                {nameof(UserLogin.LoginProvider)}_
                {nameof(UserLogin.ProviderDisplayName)}_
                {nameof(UserLogin.Date)}
            ".Replace("\n", "").Trim());
    }
}