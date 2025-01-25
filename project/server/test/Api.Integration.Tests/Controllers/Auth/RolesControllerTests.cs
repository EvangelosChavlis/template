// packages
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;

// test
using server.test.Api.Integration.Tests.TestHelpers;

namespace server.test.Api.Integration.Tests.Controllers.Auth;

public class RolesControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public RolesControllerTests(TestingWebAppFactory<Program> factory)
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
        var role = new Role
        {
            Name = "Admin",
            Description = "Administrator role with full access",
            IsActive = true
        };

        context.Roles.Add(role);
        context.SaveChanges();
    }

    [Fact]
    public async Task GetRoles_ReturnsListOfRoles()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        // Act
        var response = await _client.GetAsync("/api/auth/roles");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<ItemRoleDto>>(content);

        Assert.NotNull(result);
        Assert.Contains(result, item => item.Name == "Admin");
    }

    [Fact]
    public async Task GetRoles_ReturnsEmptyIfNoRoles()
    {
        // Arrange: Clear the database to ensure no roles exist
        var context = _factory.GetDataContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Act
        var response = await _client.GetAsync("/api/auth/roles");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<ItemRoleDto>>(content);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetRoleById_ReturnsSpecificRole()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var role = context.Roles.FirstOrDefault();
        var roleId = role?.Id;

        Assert.NotNull(role);

        // Act
        var response = await _client.GetAsync($"/api/auth/roles/{roleId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ItemResponse<ItemRoleDto>>(content);

        Assert.NotNull(result);
        Assert.Equal(roleId, result.Data!.Id);
        Assert.Equal("Admin", result.Data.Name);
        Assert.Equal("Administrator role with full access", result.Data.Description);
    }

    [Fact]
    public async Task CreateRole_ReturnsCreatedRole()
    {
        // Arrange: Create a new role DTO
        var newRole = new RoleDto(Name: "Manager", Description: "Manages user roles and permissions");

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/roles", newRole);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Role {newRole.Name} created successfully", result.Data);
    }

    [Fact]
    public async Task CreateRole_ReturnsBadRequestForInvalidData()
    {
        // Arrange: Create an invalid role DTO (e.g., missing required fields)
        var invalidRole = new RoleDto(string.Empty, "No name for role");

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/roles", invalidRole);

        // Assert
        Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
    }

    [Fact]
    public async Task UpdateRole_ReturnsUpdatedRole()
    {
        // Arrange: Clear and seed the database with a sample role
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var role = context.Roles.FirstOrDefault();
        var roleId = role?.Id;
        var updatedRole = new RoleDto(Name: "Admin", Description: "Updated description for admin role");

        Assert.NotNull(role);

        // Act
        var response = await _client.PutAsJsonAsync($"/api/auth/roles/{roleId}", updatedRole);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Role {updatedRole.Name} updated successfully", result.Data);
    }

    [Fact]
    public async Task DeleteRole_ReturnsDeletedRoleMessage()
    {
        // Arrange: Clear and seed the database with a sample role
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var role = context.Roles.FirstOrDefault();
        var roleId = role?.Id;

        Assert.NotNull(role);

        // Act
        var response = await _client.DeleteAsync($"/api/auth/roles/{roleId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Role {role.Name} deleted successfully", result.Data);
    }


    [Fact]
    public async Task InitializeRole_ReturnsRolesInitializedMessage()
    {
        // Arrange: Create a list of roles
        var roles = new List<RoleDto>
        {
            new (Name: "Manager", Description: "Manages the team"),
            new (Name: "Developer", Description: "Writes code")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/roles/initialize", roles);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result!.Data);
    }

    [Fact]
    public async Task ActivateRole_ReturnsSuccess_WhenRoleExists()
    {
        // Arrange: Clear and seed the database before the test
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var role = context.Roles.FirstOrDefault();
        var roleId = role?.Id;
        role!.IsActive = false;
        context.SaveChanges();

        Assert.NotNull(role);

        // Act: Activate the role
        var response = await _client.GetAsync($"/api/auth/roles/activate/{roleId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Role {role.Name} activated successfully", result.Data);
    }

    [Fact]
    public async Task DeactivateRole_ReturnsSuccess_WhenRoleExists()
    {
        // Arrange: Clear and seed the database before the test with an active role
        ClearAndSeedDatabase();

        var context = _factory.GetDataContext();
        var role = context.Roles.FirstOrDefault();
        var roleId = role?.Id;

        Assert.NotNull(role);

        // Act: Deactivate the role
        var response = await _client.GetAsync($"/api/auth/roles/deactivate/{roleId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Response<string>>(content);

        Assert.NotNull(result);
        Assert.Equal($"Role {role.Name} deactivated successfully" , result.Data);
    }
}