namespace server.src.Domain.Auth.Roles.Dtos;

/// <summary>
/// Data transfer object representing detailed information about a role, including its unique identifier, name, description, 
/// active status, and version for concurrency management.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
/// <param name="IsActive">Indicates whether the role is currently active.</param>
public record ListItemRoleDto(
    Guid Id,
    string Name,
    string Description,
    bool IsActive
);