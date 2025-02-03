// source
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides mapping methods for updating an existing Role model with data from an UpdateRoleDto.
/// </summary>
public static class UpdateRoleModelMapper
{
    /// <summary>
    /// Updates an existing Role model with data from an UpdateRoleDto.
    /// </summary>
    /// <param name="dto">The UpdateRoleDto containing the updated data.</param>
    /// <param name="model">The Role model that will be updated.</param>
    /// <remarks>
    /// This method modifies the provided Role model by assigning new values from the DTO,
    /// ensuring the Name and NormalizedName are updated consistently. It also generates a new version GUID.
    /// </remarks>
    public static void UpdateRoleModelMapping(this UpdateRoleDto dto, Role model)
    {
        model.Name = dto.Name;
        model.NormalizedName = dto.Name.ToUpperInvariant();
        model.Description = dto.Description;
        model.IsActive = model.IsActive;
        model.Version = Guid.NewGuid();
    }
}
