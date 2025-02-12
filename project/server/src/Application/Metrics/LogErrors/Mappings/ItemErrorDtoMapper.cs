// source
using server.src.Domain.Metrics.LogErrors.Dtos;

namespace server.src.Application.Metrics.LogErrors.Mappings;

/// <summary>
/// Provides functionality to map an error model to an ItemLogErrorDto. This is particularly useful 
/// for error handling scenarios where default values need to be returned in case of issues, such 
/// as when an error cannot be processed or is missing critical data.
/// </summary>
public class ItemErrorDtoMapper
{
    /// <summary>
    /// Maps an Error model to an ItemLogErrorDto with default values, typically used 
    /// when an error is encountered or when the expected data is unavailable. 
    /// This method returns an ItemLogErrorDto with empty or default values for each property.
    /// </summary>
    /// <returns>An ItemLogErrorDto with default values representing an error state.</returns>
    public static ItemLogErrorDto ItemLogErrorDtoMapping() => 
        new(
            Id: Guid.Empty,
            Error: string.Empty,
            StatusCode: int.MinValue,
            Instance: string.Empty,
            ExceptionType: string.Empty,
            StackTrace: string.Empty,
            Timestamp: string.Empty
        );
}
