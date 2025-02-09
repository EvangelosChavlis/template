// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Common.Models;

namespace server.src.Persistence.Common.Configuration;

public static class CommonConfiguration
{
    public static void ConfigureBaseEntityProperties<T>(this EntityTypeBuilder<T> builder) 
              where T : BaseEntity
    {
            builder.HasKey(be => be.Id);

            builder.Property(be => be.Version)
                    .IsRequired();

            builder.Property(be => be.LockUntil)
                    .IsRequired(false);

            builder.Property(be => be.UserLockedId)
                    .IsRequired(false);

            builder.Property(be => be.TenantId)
                    .IsRequired(false);

            builder.HasOne(be => be.LockedByUser)
               .WithMany()
               .HasForeignKey(be => be.UserLockedId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}