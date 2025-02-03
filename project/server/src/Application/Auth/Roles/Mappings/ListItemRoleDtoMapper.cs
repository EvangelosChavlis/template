// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides mapping methods for converting Role models into ListItemRoleDto objects.
/// </summary>
public static class ListItemRoleDtoMapper
{
    /// <summary>
    /// Maps a Role model to a ListItemRoleDto.
    /// </summary>
    /// <param name="model">The Role model that will be mapped to a ListItemRoleDto.</param>
    /// <returns>A ListItemRoleDto representing the Role model.</returns>
    public static ListItemRoleDto ListItemRoleDtoMapping(this Role model)
        => new (
            Id: model.Id,
            Name: model.Name,
            Description: model.Description,
            IsActive: model.IsActive
        );
}
