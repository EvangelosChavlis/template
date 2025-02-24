namespace server.src.Domain.Geography.Administrative.Districts.Dtos;

/// <summary>
/// DTO for a detailed view of a district. Includes all 
/// relevant details about a district, such as name, description, population, 
/// and active status.
/// </summary>
public record ItemDistrictDto(
    Guid Id,
    string Name,
    string Description,
    long Population,
    bool IsActive,
    string Municipality
);