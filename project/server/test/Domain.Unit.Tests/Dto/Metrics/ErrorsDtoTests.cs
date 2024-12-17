// source
using server.src.Domain.Dto.Metrics;

namespace server.test.Domain.Unit.Tests.Dto.Metrics;

public class ErrorsDtoTests
{
    [Fact]
    public void Should_Create_Valid_ListItemErrorDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = "Not Found";
        var statusCode = 404;
        var timestamp = DateTime.UtcNow.ToString("o");

        // Act
        var dto = new ListItemErrorDto(id, error, statusCode, timestamp);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(error, dto.Error);
        Assert.Equal(statusCode, dto.StatusCode);
        Assert.Equal(timestamp, dto.Timestamp);
    }

    [Fact]
    public void Should_Create_Valid_ItemErrorDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var error = "Internal Server Error";
        var statusCode = 500;
        var instance = "/api/resource";
        var exceptionType = "System.Exception";
        var stackTrace = "at Namespace.Class.Method() in File.cs:line 42";
        var timestamp = DateTime.UtcNow.ToString("o");

        // Act
        var dto = new ItemErrorDto(id, error, statusCode, instance, exceptionType, stackTrace, timestamp);

        // Assert
        Assert.Equal(id, dto.Id);
        Assert.Equal(error, dto.Error);
        Assert.Equal(statusCode, dto.StatusCode);
        Assert.Equal(instance, dto.Instance);
        Assert.Equal(exceptionType, dto.ExceptionType);
        Assert.Equal(stackTrace, dto.StackTrace);
        Assert.Equal(timestamp, dto.Timestamp);
    }
}