// source
using server.src.Application.Includes.Weather;

namespace server.test.Application.Unit.Tests.Includes.Metrics;

public class TelemetryIncludesTests
{
    [Fact]
    public void GetTelemetryIncludes_ShouldReturnEmptyArray()
    {
        // Act
        var includes = TelemetryIncludes.GetTelemetryIncludes();

        // Assert
        Assert.Empty(includes);
    }
}