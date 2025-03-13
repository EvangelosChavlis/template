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
        builder.HasIndex(s => s.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(State)}_
                {nameof(State.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(State)}_
                {nameof(State.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id,
                s.Code,
                s.CountryId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(State)}_
                {nameof(State.Id)}_
                {nameof(State.Code)}_
                {nameof(State.CountryId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Name,
                s.Population,
                s.Code,
                s.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(State)}_
                {nameof(State.Id)}_
                {nameof(State.Name)}_
                {nameof(State.Population)}_
                {nameof(State.Code)}_
                {nameof(State.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
