// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Contains mapping methods to transform between Role model and DTO (Data Transfer Object).
/// </summary>
public static class RoleMappings
{
    /// <summary>
    /// Maps a Role model to an ItemRoleDto.
    /// </summary>
    /// <param name="model">The Role model that will be mapped to an ItemRoleDto.</param>
    /// <returns>An ItemRoleDto representing the Role model.</returns>
    public static ItemRoleDto ItemRoleDtoMapping(this Role model)
        => new (
            Id: model.Id,
            Name: model.Name!,
            Description: model.Description,
            IsActive: model.IsActive,
            Version: model.Version
        );

    /// <summary>
    /// Maps a Warning model to a PickerRoleDto.
    /// </summary>
    /// <param name="model">The Role model that will be mapped to a PickerRoleDto.</param>
    /// <returns>A PickerRoleDto representing the Warning model with essential details for selection purposes.</returns>
    public static PickerRoleDto PickerRoleDtoMapping(
        this Role model) => new(
            Id: model.Id,
            Name: model.Name
        );

    /// <summary>
    /// Generates an error ItemRoleDto with default empty or invalid values.
    /// </summary>
    /// <returns>An ItemRoleDto containing default values, typically used for error scenarios.</returns>
    public static ItemRoleDto ErrorItemRoleDtoMapping()
        => new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IsActive: false,
            Version: Guid.Empty
        );

    /// <summary>
    /// Maps a RoleDto to a new Role model, typically for creation.
    /// </summary>
    /// <param name="dto">The RoleDto containing the data that will be used to create the Role model.</param>
    /// <returns>A Role model populated with data from the RoleDto.</returns>
    public static Role CreateRoleModelMapping(this RoleDto dto)
        => new()
        {
            Name = dto.Name,
            NormalizedName = dto.Name.ToUpperInvariant(),
            Description = dto.Description,
            IsActive = true,
            Version = Guid.NewGuid()
        };

    /// <summary>
    /// Updates an existing Role model with data from a RoleDto.
    /// </summary>
    /// <param name="dto">The RoleDto containing the updated data.</param>
    /// <param name="model">The Role model that will be updated.</param>
    public static void UpdateRoleModelMapping(this RoleDto dto, Role model)
    {
        model.Name = dto.Name;
        model.NormalizedName = dto.Name.ToUpperInvariant();
        model.Description = dto.Description;
        model.IsActive = model.IsActive;
        model.Version = Guid.NewGuid();
    }
}