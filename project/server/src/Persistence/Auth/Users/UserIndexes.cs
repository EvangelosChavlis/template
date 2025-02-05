// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Auth.Users.Models;

namespace server.src.Persistence.Auth.Users;

public class UserIndexes : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Id)
            .IsUnique();

        builder.HasIndex(u 
            => new { 
                u.Id, 
                u.FirstName, 
                u.LastName, 
                u.Email, 
                u.UserName, 
                u.PhoneNumber, 
                u.MobilePhoneNumber 
            })
            .IsUnique();
    }
}
