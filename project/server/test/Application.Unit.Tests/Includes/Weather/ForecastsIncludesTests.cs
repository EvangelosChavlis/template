// // packages
// using System.Linq.Expressions;

// // source
// using server.src.Application.Includes.Weather;

// namespace server.test.Application.Unit.Tests.Includes.Weather;

// public class ForecastsIncludesTests
// {
//     [Fact]
//     public void GetForecastsIncludes_ShouldReturnIncludeForWarning()
//     {
//         // Act
//         var includes = ForecastsIncludes.GetForecastsIncludes();

//         // Assert
//         // Verify that there is exactly one IncludeThenInclude in the result
//         Assert.Single(includes);

//         // Ensure that the Include property in IncludeThenInclude is correctly targeting the "Warning" navigation property
//         var includeExpression = includes[0].Include;
//         var body = (MemberExpression)includeExpression.Body;

//         // Ensure that the body of the expression refers to the "Warning" property
//         Assert.Equal("Warning", body.Member.Name);
//     }
// }