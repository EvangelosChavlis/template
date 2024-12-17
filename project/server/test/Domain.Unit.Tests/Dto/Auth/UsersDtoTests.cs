// source
using server.src.Domain.Dto.Auth;

namespace server.test.Domain.Unit.Tests.Dto.Auth;

public class UsersDtoTests
{
    #region ListItemUserDto Tests
    [Fact]
    public void ListItemUserDto_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var id = "123";
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var userName = "johndoe";
        var phoneNumber = "123-456-7890";
        var mobilePhoneNumber = "987-654-3210";

        // Act
        var listItemUserDto = new ListItemUserDto(id, firstName, lastName, email, userName, phoneNumber, mobilePhoneNumber);

        // Assert
        Assert.Equal(id, listItemUserDto.Id);
        Assert.Equal(firstName, listItemUserDto.FirstName);
        Assert.Equal(lastName, listItemUserDto.LastName);
        Assert.Equal(email, listItemUserDto.Email);
        Assert.Equal(userName, listItemUserDto.UserName);
        Assert.Equal(phoneNumber, listItemUserDto.PhoneNumber);
        Assert.Equal(mobilePhoneNumber, listItemUserDto.MobilePhoneNumber);
    }

    [Fact]
    public void ListItemUserDto_ShouldSupportValueEquality()
    {
        // Arrange
        var user1 = new ListItemUserDto("123", "John", "Doe", "john.doe@example.com", "johndoe", "123-456-7890", "987-654-3210");
        var user2 = new ListItemUserDto("123", "John", "Doe", "john.doe@example.com", "johndoe", "123-456-7890", "987-654-3210");

        // Act & Assert
        Assert.Equal(user1, user2);
        Assert.True(user1 == user2); // Operator should recognize equality
    }
    #endregion

    #region ItemUserDto Tests
    [Fact]
    public void ItemUserDto_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var roles = new List<string> { "Admin", "User" };
        var itemUserDto = new ItemUserDto(
            "123", "John", "Doe", "john.doe@example.com", true, "johndoe", true, "2024-12-31", "password123",
            "123 Main St", "10001", "New York", "NY", "USA", "123-456-7890", true, "987-654-3210", true,
            "Bio text", "1990-01-01", true, roles
        );

        // Assert
        Assert.Equal("123", itemUserDto.Id);
        Assert.Equal("John", itemUserDto.FirstName);
        Assert.Equal("Doe", itemUserDto.LastName);
        Assert.Equal("john.doe@example.com", itemUserDto.Email);
        Assert.True(itemUserDto.EmailConfirmed);
        Assert.Equal("johndoe", itemUserDto.UserName);
        Assert.True(itemUserDto.LockoutEnabled);
        Assert.Equal("2024-12-31", itemUserDto.LockoutEnd);
        Assert.Equal("password123", itemUserDto.InitialPassword);
        Assert.Equal("123 Main St", itemUserDto.Address);
        Assert.Equal("10001", itemUserDto.ZipCode);
        Assert.Equal("New York", itemUserDto.City);
        Assert.Equal("NY", itemUserDto.State);
        Assert.Equal("USA", itemUserDto.Country);
        Assert.Equal("123-456-7890", itemUserDto.PhoneNumber);
        Assert.True(itemUserDto.PhoneNumberConfirmed);
        Assert.Equal("987-654-3210", itemUserDto.MobilePhoneNumber);
        Assert.True(itemUserDto.MobilePhoneNumberConfirmed);
        Assert.Equal("Bio text", itemUserDto.Bio);
        Assert.Equal("1990-01-01", itemUserDto.DateOfBirth);
        Assert.True(itemUserDto.IsActive);
        Assert.Equal(2, itemUserDto.Roles.Count);
        Assert.Contains("Admin", itemUserDto.Roles);
    }

    [Fact]
    public void ItemUserDto_ShouldSupportValueEquality()
    {
        // Arrange
        var roles1 = new List<string> { "Admin", "User" };
        var roles2 = new List<string> { "Admin", "User" };

        var user1 = new ItemUserDto(
            "123", "John", "Doe", "john.doe@example.com", true, "johndoe", true, "2024-12-31", "password123",
            "123 Main St", "10001", "New York", "NY", "USA", "123-456-7890", true, "987-654-3210", true,
            "Bio text", "1990-01-01", true, roles1
        );
        var user2 = new ItemUserDto(
            "123", "John", "Doe", "john.doe@example.com", true, "johndoe", true, "2024-12-31", "password123",
            "123 Main St", "10001", "New York", "NY", "USA", "123-456-7890", true, "987-654-3210", true,
            "Bio text", "1990-01-01", true, roles2
        );

        // Act & Assert
        // Compare basic fields
        Assert.Equal(user1.Id, user2.Id);
        Assert.Equal(user1.FirstName, user2.FirstName);
        Assert.Equal(user1.LastName, user2.LastName);
        Assert.Equal(user1.Email, user2.Email);
        Assert.Equal(user1.EmailConfirmed, user2.EmailConfirmed);
        Assert.Equal(user1.UserName, user2.UserName);
        Assert.Equal(user1.LockoutEnabled, user2.LockoutEnabled);
        Assert.Equal(user1.LockoutEnd, user2.LockoutEnd);
        Assert.Equal(user1.InitialPassword, user2.InitialPassword);
        Assert.Equal(user1.Address, user2.Address);
        Assert.Equal(user1.ZipCode, user2.ZipCode);
        Assert.Equal(user1.City, user2.City);
        Assert.Equal(user1.State, user2.State);
        Assert.Equal(user1.Country, user2.Country);
        Assert.Equal(user1.PhoneNumber, user2.PhoneNumber);
        Assert.Equal(user1.PhoneNumberConfirmed, user2.PhoneNumberConfirmed);
        Assert.Equal(user1.MobilePhoneNumber, user2.MobilePhoneNumber);
        Assert.Equal(user1.MobilePhoneNumberConfirmed, user2.MobilePhoneNumberConfirmed);
        Assert.Equal(user1.Bio, user2.Bio);
        Assert.Equal(user1.DateOfBirth, user2.DateOfBirth);
        Assert.Equal(user1.IsActive, user2.IsActive);

        // Explicitly check list equality for roles
        Assert.True(user1.Roles.SequenceEqual(user2.Roles)); // Ensures the lists are compared by value, not by reference
    }

    #endregion
}
