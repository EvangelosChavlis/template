// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides mapping functionality for converting a Role model to an ItemRoleDto.
/// </summary>
public static class ItemRoleDtoMapper
{
    /// <summary>
    /// Maps a Role model to an ItemRoleDto.
    /// </summary>
    /// <param name="model">The Role model that will be mapped to an ItemRoleDto.</param>
    /// <returns>An ItemRoleDto representing the Role model with its relevant properties.</returns>
    /// <remarks>
    /// This method extracts key properties from the Role model and creates a corresponding ItemRoleDto.
    /// It includes the role's unique identifier, name, description, active status, and version.
    /// </remarks>
    public static ItemRoleDto ItemRoleDtoMapping(this Role model)
        => new (
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            IsActive: model.IsActive,
            IsLocked: model.LockUntil,
            Version: model.Version
        );
}
