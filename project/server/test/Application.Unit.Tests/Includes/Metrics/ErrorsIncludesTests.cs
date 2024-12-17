// source
using server.src.Application.Includes.Metrics;

namespace server.test.Application.Unit.Tests.Includes.Errors;

public class ErrorsIncludesTests
{
    [Fact]
    public void GetErrorsIncludes_ShouldReturnEmptyArray()
    {
        // Act
        var includes = ErrorsIncludes.GetErrorsIncludes();

        // Assert
        Assert.Empty(includes);
    }
}
