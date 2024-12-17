// packages
using System.Linq.Expressions;

// source
using server.src.Domain.Models.Auth;
using server.src.Application.Filters.Auth;

namespace server.test.Application.Unit.Tests.Filters.Auth;

public class UserFiltersTests
{
    [Fact]
    public void UserNameSorting_ShouldReturnCorrectPropertyName()
    {
        // Act
        var result = UserFilters.UserNameSorting;

        // Assert
        Assert.Equal(nameof(User.UserName), result);
    }

    [Theory]
    [InlineData("test", true, true, true, true, true, true)]
    [InlineData("unknown", false, false, false, false, false, false)]
    [InlineData("", true, true, true, true, true, true)]
    public void UsersSearchFilter_ShouldFilterUsersBasedOnInput(
        string filter,
        bool matchUserName,
        bool matchEmail,
        bool matchFirstName,
        bool matchLastName,
        bool matchPhoneNumber,
        bool matchMobilePhoneNumber)
    {
        // Arrange
        var users = new List<User>
        {
            new User { UserName = "testUser", Email = "test@example.com", FirstName = "John", LastName = "Doe", PhoneNumber = "123456789", MobilePhoneNumber = "987654321" },
            new User { UserName = "anotherUser", Email = "another@example.com", FirstName = "Jane", LastName = "Smith", PhoneNumber = "111222333", MobilePhoneNumber = "333222111" }
        };

        var searchFilter = filter.UsersSearchFilter();

        // Act
        var filteredUsers = users.AsQueryable().Where(searchFilter).ToList();

        // Assert
        Assert.Equal(
            users.Where(u =>
                (matchUserName && u.UserName!.Contains(filter)) ||
                (matchEmail && u.Email!.Contains(filter)) ||
                (matchFirstName && u.FirstName.Contains(filter)) ||
                (matchLastName && u.LastName.Contains(filter)) ||
                (matchPhoneNumber && u.PhoneNumber!.Contains(filter)) ||
                (matchMobilePhoneNumber && u.MobilePhoneNumber.Contains(filter))
            ),
            filteredUsers
        );
    }

    [Fact]
    public void UserMatchFilters_ShouldReturnArrayWithProvidedFilter()
    {
        // Arrange
        Expression<Func<User, bool>> filter = u => u.UserName == "testUser";

        // Act
        var filters = filter.UserMatchFilters();

        // Assert
        Assert.NotNull(filters);
        Assert.Single(filters);
        Assert.Equal(filter, filters[0]);
    }

    [Fact]
    public void UserMatchFilters_ShouldHandleNullFilterGracefully()
    {
        // Arrange
        Expression<Func<User, bool>>? filter = null;

        // Act
        var filters = filter.UserMatchFilters();

        // Assert
        Assert.NotNull(filters);
        Assert.Single(filters);
        Assert.Null(filters[0]);
    }
}