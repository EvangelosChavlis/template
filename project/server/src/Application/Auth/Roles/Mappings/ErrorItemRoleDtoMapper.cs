// source
using server.src.Domain.Auth.Roles.Dtos;

namespace server.src.Application.Auth.Roles.Mappings;

/// <summary>
/// Provides a mapping method for generating an error representation of an ItemRoleDto.
/// </summary>
public static class ErrorItemRoleDtoMapper
{
    /// <summary>
    /// Generates an ItemRoleDto with default or invalid values, typically used for error handling scenarios.
    /// </summary>
    /// <returns>
    /// An ItemRoleDto containing default values such as an empty GUID for Id, 
    /// an empty string for Name and Description, and false for IsActive.
    /// </returns>
    /// <remarks>
    /// This method is useful when returning a fallback or placeholder object in case of 
    /// failures, missing data, or validation errors.
    /// </remarks>
    public static ItemRoleDto ErrorItemRoleDtoMapping()
        => new (
            Id: Guid.Empty,
            Name: string.Empty,
            Description: string.Empty,
            IsActive: false,
            IsLocked: true,
            Version: Guid.Empty
        );
}
