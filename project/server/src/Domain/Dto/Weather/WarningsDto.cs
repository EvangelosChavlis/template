namespace server.src.Domain.Dto.Weather;

/// <summary>
/// Represents a simplified warning item that contains basic details 
/// about the warning, including the number of related forecasts.
/// </summary>
public record ListItemWarningDto(
    Guid Id, 
    string Name, 
    string Description,
    int Count
);

/// <summary>
/// Represents a warning item used in a picker, containing only the warning's
/// ID and name for selection purposes.
/// </summary>
public record PickerWarningDto(
    Guid Id, 
    string Name
);

/// <summary>
/// Represents a detailed warning item that includes the warning's name, 
/// description, and a list of related forecasts.
/// </summary>
public record ItemWarningDto(
    Guid Id,
    string Name, 
    string Description,
    Guid Version
);

/// <summary>
/// Represents a warning data transfer object containing the name 
/// and description of the warning.
/// </summary>
public record WarningDto(
    string Name,
    string Description,
    string RecommendedActions,
    Guid Version
);
