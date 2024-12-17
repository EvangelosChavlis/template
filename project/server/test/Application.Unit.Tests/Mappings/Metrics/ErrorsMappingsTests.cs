// source
using server.src.Application.Mappings.Metrics;
using server.src.Domain.Models.Errors;
using server.src.Domain.Extensions;

namespace server.tests.Application.Mappings.Metrics;

public class ErrorsMappingsTests
{
    [Fact]
    public void ListItemErrorDtoMapping_MapsLogErrorToListItemErrorDto()
    {
        // Arrange
        var logError = new LogError
        {
            Id = Guid.NewGuid(),
            Error = "An error occurred",
            StatusCode = 500,
            Timestamp = new DateTime(2023, 12, 4, 10, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var result = logError.ListItemErrorDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(logError.Id, result.Id);
        Assert.Equal(logError.Error, result.Error);
        Assert.Equal(logError.StatusCode, result.StatusCode);
        Assert.Equal(logError.Timestamp.GetFullLocalDateTimeString(), result.Timestamp);
    }

    [Fact]
    public void ItemErrorDtoMapping_MapsLogErrorToItemErrorDto()
    {
        // Arrange
        var logError = new LogError
        {
            Id = Guid.NewGuid(),
            Error = "An error occurred",
            StatusCode = 500,
            Instance = "/api/resource",
            ExceptionType = "System.Exception",
            StackTrace = "StackTrace details here...",
            Timestamp = new DateTime(2023, 12, 4, 10, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var result = logError.ItemErrorDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(logError.Id, result.Id);
        Assert.Equal(logError.Error, result.Error);
        Assert.Equal(logError.StatusCode, result.StatusCode);
        Assert.Equal(logError.Instance, result.Instance);
        Assert.Equal(logError.ExceptionType, result.ExceptionType);
        Assert.Equal(logError.StackTrace, result.StackTrace);
        Assert.Equal(logError.Timestamp.GetFullLocalDateTimeString(), result.Timestamp);
    }
}