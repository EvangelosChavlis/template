// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Indexes.Auth;

public class UserLogoutIndexes : IEntityTypeConfiguration<UserLogout>
{
    public void Configure(EntityTypeBuilder<UserLogout> builder)
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
