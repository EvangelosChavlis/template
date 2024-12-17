// packages
using Newtonsoft.Json;
using System.Net.Http.Json;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Domain.Dto.Common;

// test
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Auth;

public class UsersControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public UsersControllerTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    private void ClearAndSeedDatabase()
    {
        var context = _factory.GetDataContext();

        // Clear the database (no transaction needed)
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();

        // Seed fresh data for each test
        var admin = new Role
        {
            Name = "Admin",
            Description = "Administrator role with full access",
            IsActive = true
        };

        context.Roles.Add(admin);

        context.SaveChanges();

        // Create a user and assign the role
        var user = new User
        {
            UserName = "adminuser",
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@example.com",
            InitialPassword = "Password123!",
            PasswordHash = "Password123!",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            NormalizedUserName = "ADMINUSER",
            EmailConfirmed = true,
            LockoutEnabled = false,
            IsActive = true,
            DateOfBirth = DateTime.Now.AddYears(-30),
            MobilePhoneNumber = "1234567890",
            MobilePhoneNumberConfirmed = true,
            Address = "123 Admin Street",
            ZipCode = "12345",
            City = "Admin City",
            State = "Admin State",
            Country = "Admin Country",
            Bio = "Admin User",
        };

        // You can hash the password properly if needed, but for simplicity, assuming it's done
        user.UserRoles = new List<UserRole>
        {
            new UserRole
            {
                RoleId = admin.Id,
                UserId = user.Id
            }
        };

        context.Users.Add(user);
        context.SaveChanges();
    }


    [Fact]
    public async Task GetUsers_ShouldReturnOkResult_WhenUsersExist()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/auth/users");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<List<ListItemUserDto>>>(content);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Data!);
        Assert.Contains(result.Data!, user => user.Email == "admin@example.com");
    }

    [Fact]
    public async Task ActivateUser_ReturnsSuccess_WhenRoleExists()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;
        user!.IsActive = false;
        context.SaveChanges();

        Assert.NotNull(user);

        // Act: Activate the role
        var response = await _client.GetAsync($"/api/auth/users/activate/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {user.UserName} activated successfully", result.Data);
    }

    [Fact]
    public async Task DeactivateUser_ReturnsSuccess_WhenRoleExists()
    {
        // Arrange: Clear and seed the database before the test with an active role
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);

        // Act: Deactivate the role
        var response = await _client.GetAsync($"/api/auth/users/deactivate/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {user.UserName} deactivated successfully" , result.Data);
    }

    [Fact]
    public async Task LockUser_ReturnsSuccess_WhenUserExists()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);
        user!.LockoutEnabled = false;
        context.SaveChanges();

        // Act: Lock the user
        var response = await _client.GetAsync($"/api/auth/users/lock/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {user.UserName} locked successfully", result.Data);

        // Verify the user is locked in the database
        context = _factory.GetDataContext(); // Refresh the context
        user = context.Users.Find(userId);
        Assert.NotNull(user);
        Assert.True(user!.LockoutEnabled);
    }

    [Fact]
    public async Task UnlockUser_ReturnsSuccess_WhenUserExists()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);
        user!.LockoutEnabled = true;
        context.SaveChanges();

        // Act: Unlock the user
        var response = await _client.GetAsync($"/api/auth/users/unlock/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {user.UserName} unlocked successfully", result.Data);

        // Verify the user is unlocked in the database
        context = _factory.GetDataContext(); // Refresh the context
        user = context.Users.Find(userId);
        Assert.NotNull(user);
        Assert.False(user!.LockoutEnabled);
    }

    [Fact]
    public async Task ConfirmEmailUser_ReturnsSuccess_WhenUserExists()
    {
        // Arrange: Clear and seed the database
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);
        user!.EmailConfirmed = false;
        context.SaveChanges();

        // Act: Confirm the email
        var response = await _client.GetAsync($"/api/auth/users/confirm/email/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Email {user.Email} confirmed successfully", result.Data);

        // Verify the email is confirmed in the database
        context = _factory.GetDataContext(); // Refresh the context
        user = context.Users.Find(userId);
        Assert.NotNull(user);
        Assert.True(user!.EmailConfirmed);
    }

    [Fact]
    public async Task ConfirmPhoneNumberUser_ReturnsSuccess_WhenUserExists()
    {
        // Arrange: Clear and seed the database
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);
        user!.PhoneNumberConfirmed = false;
        context.SaveChanges();

        // Act: Confirm the phone number
        var response = await _client.GetAsync($"/api/auth/users/confirm/phoneNumber/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Phone number {user.PhoneNumber} confirmed successfully", result.Data);

        // Verify the phone number is confirmed in the database
        context = _factory.GetDataContext(); // Refresh the context
        user = context.Users.Find(userId);
        Assert.NotNull(user);
        Assert.True(user!.PhoneNumberConfirmed);
    }

    [Fact]
    public async Task ConfirmMobilePhoneNumberUser_ReturnsSuccess_WhenUserExists()
    {
        // Arrange: Clear and seed the database
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);
        user!.MobilePhoneNumberConfirmed = false;
        context.SaveChanges();

        // Act: Confirm the mobile phone number
        var response = await _client.GetAsync($"/api/auth/users/confirm/mobilePhoneNumber/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Mobile phone number {user.MobilePhoneNumber} confirmed successfully", result.Data);

        // Verify the mobile phone number is confirmed in the database
        context = _factory.GetDataContext(); // Refresh the context
        user = context.Users.Find(userId);
        Assert.NotNull(user);
        Assert.True(user!.MobilePhoneNumberConfirmed);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange: Seed the database with a user
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var user = context.Users.FirstOrDefault();
        var userId = user?.Id;

        Assert.NotNull(user);

        // Act: Delete the user
        var response = await _client.DeleteAsync($"/api/auth/users/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {user.UserName} deleted successfully", result.Data);

        // Verify the user is deleted in the database
        context = _factory.GetDataContext(); // Refresh the context
        var deletedUser = context.Users.Find(userId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnSuccess_WhenUserExistsAndDataIsValid()
    {
        // Arrange: Seed the database with a user
        ClearAndSeedDatabase();
        var context = _factory.GetDataContext();
        var existingUser = context.Users.FirstOrDefault();

        Assert.NotNull(existingUser);

        var userId = existingUser!.Id;

        var updatedUserDto = new UserDto(
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            UserName: "johndoe123",
            Password: "P@ssw0rd123",
            Address: "123 Main St",
            ZipCode: "12345",
            City: "New York",
            State: "NY",
            Country: "USA",
            PhoneNumber: "+1234567890",
            MobilePhoneNumber: "+1234567890",
            Bio: "Bio of John Doe.",
            DateOfBirth: new DateTime(1990, 1, 1)
        );

        // Act: Send PUT request to update user
        var response = await _client.PutAsJsonAsync($"/api/auth/users/{userId}", updatedUserDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"User {updatedUserDto.UserName} updated successfully", result.Data);

        // Verify updates in the database
        context = _factory.GetDataContext(); // Refresh context
        var updatedUser = context.Users.FirstOrDefault(u => u.Id == userId);

        Assert.NotNull(updatedUser);
        Assert.Equal(updatedUserDto.FirstName, updatedUser!.FirstName);
        Assert.Equal(updatedUserDto.LastName, updatedUser.LastName);
        Assert.Equal(updatedUserDto.Email, updatedUser.Email);
        Assert.Equal(updatedUserDto.UserName, updatedUser.UserName);
        Assert.Equal(updatedUserDto.Address, updatedUser.Address);
        Assert.Equal(updatedUserDto.ZipCode, updatedUser.ZipCode);
        Assert.Equal(updatedUserDto.City, updatedUser.City);
        Assert.Equal(updatedUserDto.State, updatedUser.State);
        Assert.Equal(updatedUserDto.Country, updatedUser.Country);
        Assert.Equal(updatedUserDto.PhoneNumber, updatedUser.PhoneNumber);
        Assert.Equal(updatedUserDto.MobilePhoneNumber, updatedUser.MobilePhoneNumber);
        Assert.Equal(updatedUserDto.Bio, updatedUser.Bio);
    }

    // [Fact]
    // public async Task Register_ShouldReturnSuccess_WhenDataIsValid()
    // {
    //     // Arrange
    //     ClearAndSeedDatabase();
    //     var newUserDto = new UserDto(
    //         FirstName: "John",
    //         LastName: "Doe",
    //         Email: "john.doe@example.com",
    //         UserName: "johndoe123",
    //         Password: "P@ssw0rd123",
    //         Address: "123 Main St",
    //         ZipCode: "12345",
    //         City: "New York",
    //         State: "NY",
    //         Country: "USA",
    //         PhoneNumber: "+1234567890",
    //         MobilePhoneNumber: "+1234567890",
    //         Bio: "Bio of John Doe.",
    //         DateOfBirth: new DateTime(1990, 1, 1)
    //     );

    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/auth/user/register", newUserDto);

    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

    //     Assert.NotNull(result);
    //     Assert.True(result.Success);
    //     Assert.Equal($"User {newUserDto.UserName} registered successfully", result.Data);

    //     // Verify in the database
    //     var context = _factory.GetDataContext();
    //     var user = context.Users.FirstOrDefault(u => u.Email == newUserDto.Email);
    //     Assert.NotNull(user);
    //     Assert.Equal(newUserDto.UserName, user.UserName);
    // }

    // [Fact]
    // public async Task CreateUser_ShouldReturnSuccess_WhenDataIsValid()
    // {
    //     // Arrange
    //     ClearAndSeedDatabase();
    //     var newUserDto = new UserDto(
    //         FirstName: "John",
    //         LastName: "Doe",
    //         Email: "john.doe@example.com",
    //         UserName: "johndoe123",
    //         Password: "P@ssw0rd123",
    //         Address: "123 Main St",
    //         ZipCode: "12345",
    //         City: "New York",
    //         State: "NY",
    //         Country: "USA",
    //         PhoneNumber: "+1234567890",
    //         MobilePhoneNumber: "+1234567890",
    //         Bio: "Bio of John Doe.",
    //         DateOfBirth: new DateTime(1990, 1, 1)
    //     );

    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/auth/user/create", newUserDto);

    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

    //     Assert.NotNull(result);
    //     Assert.True(result.Success);
    //     Assert.Equal($"User {newUserDto.UserName} registered successfully", result.Data);

    //     // Verify in the database
    //     var context = _factory.GetDataContext();
    //     var user = context.Users.FirstOrDefault(u => u.Email == newUserDto.Email);
    //     Assert.NotNull(user);
    //     Assert.Equal(newUserDto.UserName, user.UserName);
    // }

    // [Fact]
    // public async Task AssignRoleToUser_ShouldReturnSuccess_WhenUserAndRoleExist()
    // {
    //     // Arrange: Seed the database with a user and a role
    //     ClearAndSeedDatabase();
    //     var context = _factory.GetDataContext();

    //     var userRole = new Role
    //     {
    //         Name = "User",
    //         Description = "User role without full access",
    //         IsActive = true
    //     };

    //     context.Roles.Add(userRole);

    //     context.SaveChanges();

    //     var user = context.Users.FirstOrDefault();
    //     var role = context.Roles.FirstOrDefault(r => r.Name!.Equals("User"));

    //     Assert.NotNull(user);
    //     Assert.NotNull(role);

    //     var userId = user!.Id;
    //     var roleId = role!.Id;

    //     // Act: Assign the role to the user
    //     var response = await _client.GetAsync($"/api/auth/assign/{userId}/{roleId}");

    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

    //     Assert.NotNull(result);
    //     Assert.Equal($"Role {role.Name} assigned to user {user.UserName} successfully", result.Data);

    //     // Verify the role assignment in the database
    //     context = _factory.GetDataContext(); // Refresh context
    //     var userRoles = context.UserRoles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
    //     Assert.NotNull(userRoles);
    // }



    // [Fact]
    // public async Task Register_ShouldReturn201Created_WhenValidUserDataIsProvided()
    // {
    //     // Arrange
    //     var userDto = new UserDto(
    //         FirstName: "John",
    //         LastName: "Doe",
    //         Email: "johndoe@example.com",
    //         UserName: "john_doe",
    //         Password: "Password123!",
    //         Address: "123 Main St",
    //         ZipCode: "12345",
    //         City: "New York",
    //         State: "NY",
    //         Country: "USA",
    //         PhoneNumber: "123-456-7890",
    //         MobilePhoneNumber: "987-654-3210",
    //         Bio: "New user",
    //         DateOfBirth: DateTime.Now.AddYears(-25)
    //     );


    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/auth/register", userDto);

    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     var content = await response.Content.ReadAsStringAsync();
    //     var result = JsonConvert.DeserializeObject<CommandResponse<string>>(content);

    //     Assert.NotNull(result);
    //     Assert.Equal($"User {userDto.UserName} registered successfully", result.Data);
    // }
}
