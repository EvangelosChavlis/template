namespace server.src.Domain.Auth.Roles.Dtos;

/// <summary>
/// Data transfer object representing a role with its basic properties.
/// </summary>
/// <param name="Name">The name of the role.</param>
/// <param name="Description">A brief description of the role.</param>
public record CreateRoleDto(
    string Name,
    string Description
);