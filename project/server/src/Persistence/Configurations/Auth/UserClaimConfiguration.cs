// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Auth;

namespace server.src.Persistence.Configurations.Auth;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    private readonly string _tableName;
    private readonly string _schema;

    public UserClaimConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.ClaimType)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(uc => uc.ClaimValue)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.UserClaims)
            .HasForeignKey(uc => uc.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }

}