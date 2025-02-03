namespace server.src.Domain.Auth.Roles.Dtos;

/// <summary>
/// Data transfer object for selecting a role, typically used in dropdowns or pickers.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
/// <param name="Name">The name of the role.</param>
public record PickerRoleDto(
    Guid Id,
    string Name
);