namespace server.src.Domain.Dto.Auth;

/// <summary>
/// Data transfer object representing a role with its basic properties.
/// </summary>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
public record RoleDto(
    string Name,
    string Description
);

/// <summary>
/// Data transfer object representing detailed information about a role.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
/// <param name="IsActive">Indicates whether the role is currently active.</param>
public record ItemRoleDto(
    string Id,
    string Name,
    string Description,
    bool IsActive
);
