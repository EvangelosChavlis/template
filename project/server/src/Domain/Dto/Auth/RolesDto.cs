namespace server.src.Domain.Dto.Auth;

/// <summary>
/// Data transfer object representing a role with its basic properties.
/// </summary>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
/// /// <param name="Version">The version of the role for concurrency control during updates.</param>
public record RoleDto(
    string Name,
    string Description,
    Guid Version
);

/// <summary>
/// Data transfer object representing detailed information about a role, including its unique identifier, name, description, 
/// active status, and version for concurrency management.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
/// <param name="IsActive">Indicates whether the role is currently active.</param>
/// <param name="Version">The version of the role for concurrency control during updates.</param>
public record ItemRoleDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    Guid Version
);


/// <summary>
/// Data transfer object for selecting a role, typically used in dropdowns or pickers.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
/// <param name="Name">The name of the role.</param>
public record PickerRoleDto(
    Guid Id,
    string Name
);