// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides mapping methods for converting CreateRoleDto objects into Role models.
/// </summary>
public static class CreateRoleModelMapper
{
    /// <summary>
    /// Maps a CreateRoleDto to a new Role model, typically for creation.
    /// </summary>
    /// <param name="dto">The CreateRoleDto containing the data that will be used to create the Role model.</param>
    /// <returns>A new Role model populated with data from the CreateRoleDto.</returns>
    public static Role CreateRoleModelMapping(this CreateRoleDto dto)
        => new()
        {
            Name = dto.Name,
            NormalizedName = dto.Name.ToUpperInvariant(),
            Description = dto.Description,
            IsActive = true,
            Version = Guid.NewGuid()
        };
}