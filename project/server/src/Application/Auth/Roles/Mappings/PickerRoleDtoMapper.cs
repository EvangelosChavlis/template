// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides mapping functionality for converting a Role model to a PickerRoleDto.
/// </summary>
public static class PickerRoleDtoMapper
{
    /// <summary>
    /// Maps a Role model to a PickerRoleDto.
    /// </summary>
    /// <param name="model">The Role model that will be mapped to a PickerRoleDto.</param>
    /// <returns>
    /// A PickerRoleDto containing minimal details (Id and Name) of the Role model, 
    /// typically used for selection or dropdown lists.
    /// </returns>
    /// <remarks>
    /// This method extracts only the essential properties (Id and Name) from the Role model 
    /// for lightweight data transfer, making it suitable for UI selection components.
    /// </remarks>
    public static PickerRoleDto PickerRoleDtoMapping(this Role model) 
        => new(
            Id: model.Id,
            Name: model.Name
        );
}