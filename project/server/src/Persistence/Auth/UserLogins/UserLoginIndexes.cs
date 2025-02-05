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
        builder.HasIndex(ul => ul.Id)
            .IsUnique();

        builder.HasIndex(ul
            => new { 
                ul.Id, 
                ul.LoginProvider, 
                ul.ProviderDisplayName, 
                ul.Date
            })
            .IsUnique();
    }
}
