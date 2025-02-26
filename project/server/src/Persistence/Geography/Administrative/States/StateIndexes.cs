// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Persistence.Geography.Administrative.States;

public class StateIndexes : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasIndex(r => r.Id)
            .IsUnique();

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Name,
                s.Population,
                s.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(State)}_
                {nameof(State.Id)}_
                {nameof(State.Name)}_
                {nameof(State.Population)}_
                {nameof(State.IsActive)}
            ".Replace("\n", "").Trim());
    }
}
