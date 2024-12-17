// source
using server.src.Application.Includes.Auth;

namespace server.test.Application.Unit.Tests.Includes.Auth;

public class UserIncludesTests
{
    [Fact]
    public void GetUsersIncludes_ShouldReturnEmptyArray()
    {
        // Act
        var includes = UserIncludes.GetUsersIncludes();

        // Assert
        Assert.Empty(includes);
    }
}
