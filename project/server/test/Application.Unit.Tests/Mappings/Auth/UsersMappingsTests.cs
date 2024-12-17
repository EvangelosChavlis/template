// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Extensions;

namespace server.tests.Application.Mappings.Auth;

public class UsersMappingsTests
{
    [Fact]
    public void ListItemUserDtoMapping_ShouldMapUserToListItemUserDtoCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            UserName = "johndoe",
            PhoneNumber = "123456789",
            MobilePhoneNumber = "987654321"
        };

        // Act
        var result = user.ListItemUserDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal(user.PhoneNumber, result.PhoneNumber);
        Assert.Equal(user.MobilePhoneNumber, result.MobilePhoneNumber);
    }

    [Fact]
    public void ItemUserDtoMapping_ShouldMapUserToItemUserDtoCorrectly()
    {
        // Arrange
        var userRoles = new List<UserRole>
        {
            new() { Role = new Role { Name = "Admin" } },
            new() { Role = new Role { Name = "User" } }
        };

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            EmailConfirmed = true,
            UserName = "johndoe",
            LockoutEnabled = false,
            LockoutEnd = DateTime.UtcNow,
            InitialPassword = "initial_password",
            Address = "123 Main St",
            ZipCode = "12345",
            City = "Metropolis",
            State = "NY",
            Country = "USA",
            PhoneNumber = "123456789",
            PhoneNumberConfirmed = false,
            MobilePhoneNumber = "987654321",
            MobilePhoneNumberConfirmed = false,
            Bio = "A brief bio",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            IsActive = true,
            UserRoles = userRoles
        };

        // Act
        var result = user.ItemUserDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName, result.LastName);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.EmailConfirmed, result.EmailConfirmed);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal(user.LockoutEnabled, result.LockoutEnabled);
        Assert.Equal(user.LockoutEnd.ToString(), result.LockoutEnd);
        Assert.Equal(user.InitialPassword, result.InitialPassword);
        Assert.Equal(user.Address, result.Address);
        Assert.Equal(user.ZipCode, result.ZipCode);
        Assert.Equal(user.City, result.City);
        Assert.Equal(user.State, result.State);
        Assert.Equal(user.Country, result.Country);
        Assert.Equal(user.PhoneNumber, result.PhoneNumber);
        Assert.Equal(user.PhoneNumberConfirmed, result.PhoneNumberConfirmed);
        Assert.Equal(user.MobilePhoneNumber, result.MobilePhoneNumber);
        Assert.Equal(user.MobilePhoneNumberConfirmed, result.MobilePhoneNumberConfirmed);
        Assert.Equal(user.Bio, result.Bio);
        Assert.Equal(user.DateOfBirth.GetLocalDateTimeString(), result.DateOfBirth);
        Assert.Equal(user.IsActive, result.IsActive);
        Assert.Equal(userRoles.Select(ur => ur.Role.Name!).ToList(), result.Roles);
    }

    [Fact]
    public void AuthenticatedUserDtoMapping_ShouldMapToAuthenticatedUserDtoCorrectly()
    {
        // Arrange
        var userName = "johndoe";
        var token = "sample_token";

        // Act
        var result = userName.AuthenticatedUserDtoMapping(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userName, result.UserName);
        Assert.Equal(token, result.Token);
    }

    [Fact]
    public void CreateUserModelMapping_ShouldMapUserDtoToUserCorrectly()
    {
        // Arrange
        var dto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe",
            Password: "secure_password",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "Metropolis",
            State: "NY",
            Country: "USA",
            PhoneNumber: "123456789",
            MobilePhoneNumber: "987654321",
            Bio: "A brief bio",
            DateOfBirth: DateTime.UtcNow.AddYears(-30)
        );


        // Act
        var result = dto.CreateUserModelMapping(false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.Equal(dto.LastName, result.LastName);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.UserName, result.UserName);
        Assert.Equal(dto.Password, result.InitialPassword);
        Assert.False(result.EmailConfirmed);
        Assert.False(result.LockoutEnabled);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.ZipCode, result.ZipCode);
        Assert.Equal(dto.City, result.City);
        Assert.Equal(dto.State, result.State);
        Assert.Equal(dto.Country, result.Country);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.False(result.PhoneNumberConfirmed);
        Assert.Equal(dto.MobilePhoneNumber, result.MobilePhoneNumber);
        Assert.False(result.MobilePhoneNumberConfirmed);
        Assert.Equal(dto.Bio, result.Bio);
        Assert.Equal(dto.DateOfBirth, result.DateOfBirth);
    }
}