// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.UserLogouts.Models;

namespace server.src.Persistence.Auth.UserLogouts;

public class UserLogoutIndexes : IEntityTypeConfiguration<UserLogout>
{
    public void Configure(EntityTypeBuilder<UserLogout> builder)
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
                {nameof(UserLogout)}_
                {nameof(UserLogout.Id)}_
                {nameof(UserLogout.LoginProvider)}_
                {nameof(UserLogout.ProviderDisplayName)}_
                {nameof(UserLogout.Date)}
            ".Replace("\n", "").Trim());
    }
}