// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;
using server.src.Application.Mappings.Auth;

namespace server.tests.Application.Mappings.Auth;

public class RolesMappingsTests
{
    [Fact]
    public void ItemRoleDtoMapping_ShouldMapRoleToItemRoleDtoCorrectly()
    {
        // Arrange
        var role = new Role
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Admin",
            Description = "Administrator role",
            IsActive = true
        };

        // Act
        var result = role.ItemRoleDtoMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(role.Id, result.Id);
        Assert.Equal(role.Name, result.Name);
        Assert.Equal(role.Description, result.Description);
        Assert.Equal(role.IsActive, result.IsActive);
    }

    [Fact]
    public void CreateRoleModelMapping_ShouldCreateRoleModelCorrectly()
    {
        // Arrange
        var dto = new RoleDto(Name: "Manager", Description: "Manager role");

        // Act
        var result = dto.CreateRoleModelMapping();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Description, result.Description);
        Assert.True(result.IsActive);
    }

    [Fact]
    public void UpdateRoleModelMapping_ShouldUpdateRoleModelCorrectly()
    {
        // Arrange
        var role = new Role
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Admin",
            Description = "Old description",
            IsActive = false
        };

        var dto = new RoleDto (Name: "Super Admin", Description: "Updated description");

        // Act
        dto.UpdateRoleModelMapping(role);

        // Assert
        Assert.Equal(dto.Name, role.Name);
        Assert.Equal(dto.Description, role.Description);
        Assert.False(role.IsActive); // `IsActive` should not change
    }
}