// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Configurations.Auth;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly string _tableName;
    private readonly string _schema;

    public UserConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Address)
            .IsRequired();

        builder.Property(u => u.ZipCode)
            .IsRequired();

        builder.Property(u => u.City)
            .IsRequired();

        builder.Property(u => u.State)
            .IsRequired();

        builder.Property(u => u.Country)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .IsRequired();

        builder.Property(u => u.MobilePhoneNumber)
            .IsRequired();

        builder.Property(u => u.Bio)
            .HasMaxLength(500);

        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        builder.Property(u => u.PasswordResetToken)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(u => u.PasswordResetTokenExpiry)
            .IsRequired(false);

        builder.Property(u => u.TwoFactorToken)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(u => u.TwoFactorTokenExpiry)
            .IsRequired(false);

        builder.Property(u => u.Version)
            .IsRequired(true);

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasMany(u => u.TelemetryRecords)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserLogins)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserLogouts)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserClaims)
            .WithOne(uc => uc.User)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AuditLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}
