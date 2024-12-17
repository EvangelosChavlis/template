// packages
using System.Linq.Expressions;

// source
using server.src.Application.Includes.Weather;

namespace server.test.Application.Unit.Tests.Includes.Weather;

public class WarningsIncludesTests
{
    [Fact]
    public void GetWarningsIncludes_ShouldReturnIncludeForForecasts()
    {
        // Act
        var includes = WarningsIncludes.GetWarningsIncludes();

        // Assert
        // Verify that there is exactly one IncludeThenInclude in the result
        Assert.Single(includes);

        // Extract the Include expression from the first IncludeThenInclude
        var includeExpression = includes[0].Include;
        var body = (MemberExpression)includeExpression.Body;

        // Ensure that the body of the expression refers to the "Forecasts" property
        Assert.Equal("Forecasts", body.Member.Name);
    }
}