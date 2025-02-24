// // packages
// using System.Net;
// using Microsoft.AspNetCore.Identity;
// using MockQueryable;
// using Moq;

// // source
// using server.src.Application.Services.Auth;
// using server.src.Domain.Dto.Auth;
// using server.src.Domain.Exceptions;
// using server.src.Domain.Models.Auth;

// namespace server.test.Application.Unit.Tests.Services.Auth;

// public class RoleServiceTests
// {
//     private readonly Mock<RoleManager<Role>> _roleManagerMock;
//     private readonly RoleService _roleService;

//     public RoleServiceTests()
//     {
//         var roleStoreMock = new Mock<IRoleStore<Role>>();
//         _roleManagerMock = new Mock<RoleManager<Role>>(roleStoreMock.Object, null!, null!, null!, null!);
//         _roleService = new RoleService(_roleManagerMock.Object);
//     }

//     [Fact]
//     public async Task GetRolesService_ReturnsListOfRoles()
//     {
//         // Arrange
//         var roles = new List<Role>
//         {
//             new() 
//             { 
//                 Id = Guid.NewGuid().ToString(),
//                 Name = "Admin", 
//                 IsActive = true,
//                 Description = "This is a description" 
//             },
//             new() 
//             {
//                 Id = Guid.NewGuid().ToString(), 
//                 Name = "User",
//                 IsActive = true, 
//                 Description = "This is a description"  
//             }
//         }.AsQueryable().BuildMock();

//         _roleManagerMock.Setup(rm => rm.Roles).Returns(roles);

//         // Act
//         var result = await _roleService.GetRolesService();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(2, result.Count);
//         Assert.Contains(result, r => r.Name == "Admin");
//         Assert.Contains(result, r => r.Name == "User");
//     }

    

//     [Fact]
//     public async Task GetRoleByIdService_ReturnsRole_WhenFound()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role 
//         { 
//             Id = id,
//             Name = "Admin",
//             IsActive = true,
//             Description = "This is a description" 
//         };
//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);

//         // Act
//         var result = await _roleService.GetRoleByIdService(id);

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal("Admin", result.Data!.Name);
//     }

//     [Fact]
//     public async Task GetRoleByIdService_ThrowsException_WhenRoleNotFound()
//     {
//         // Arrange
//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(Guid.NewGuid().ToString())).ReturnsAsync((Role)null!);

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.GetRoleByIdService("1"));
//         Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
//         Assert.Equal("Role not found.", exception.Message);
//     }

//     [Fact]
//     public async Task CreateRoleService_CreatesRoleSuccessfully()
//     {
//         // Arrange
//         var dto = new RoleDto(Name: "Admin", Description: "This is a description");
//         _roleManagerMock.Setup(rm => rm.FindByNameAsync("Admin")).ReturnsAsync((Role)null!);
//         _roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<Role>())).ReturnsAsync(IdentityResult.Success);

//         // Act
//         var result = await _roleService.CreateRoleService(dto);

//         // Assert
//         Assert.True(result.Success);
//         Assert.Contains("Role Admin created successfully", result.Data);
//     }

//     [Fact]
//     public async Task CreateRoleService_ThrowsException_WhenRoleExists()
//     {
//         // Arrange
//         var existingRole = new Role 
//         { 
//             Name = "Admin", 
//             Description = "This is a description"
//         };
//         var dto = new RoleDto(Name: "Admin", Description: "This is a description");

//         _roleManagerMock.Setup(rm => rm.FindByNameAsync("Admin")).ReturnsAsync(existingRole);

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.CreateRoleService(dto));
//         Assert.Equal("Role with name Admin already exists", exception.Message);
//     }

//     [Fact]
//     public async Task UpdateRoleService_UpdatesRoleSuccessfully()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role 
//         { 
//             Id = id, 
//             Name = "Admin", 
//             IsActive = true,
//             Description = "This is a description" 
//         };
//         var dto = new RoleDto(Name: "Admin", Description: "This is a description");

//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);
//         _roleManagerMock.Setup(rm => rm.UpdateAsync(role)).ReturnsAsync(IdentityResult.Success);

//         // Act
//         var result = await _roleService.UpdateRoleService(id, dto);

//         // Assert
//         Assert.True(result.Success);
//         Assert.Contains("Role Admin updated successfully", result.Data);
//     }

//     [Fact]
//     public async Task ActivateRoleService_ActivatesRoleSuccessfully()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role
//         {
//             Id = id,
//             Name = "Admin",
//             IsActive = false,
//             Description = "This is a description" 
//         };

//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);
//         _roleManagerMock.Setup(rm => rm.UpdateAsync(role)).ReturnsAsync(IdentityResult.Success);

//         // Act
//         var result = await _roleService.ActivateRoleService(id);

//         // Assert
//         Assert.True(result.Success);
//         Assert.Contains("Role Admin activated successfully", result.Data);
//         Assert.True(role.IsActive); // Ensure the role was updated to active
//     }

//     [Fact]
//     public async Task ActivateRoleService_ThrowsException_WhenRoleNotFound()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync((Role)null!);

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.ActivateRoleService(id));
//         Assert.Equal((int)HttpStatusCode.NotFound, exception.StatusCode);
//         Assert.Equal("Role not found.", exception.Message);
//     }

//     [Fact]
//     public async Task ActivateRoleService_ThrowsException_WhenRoleAlreadyActive()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role
//         {
//             Id = id,
//             Name = "Admin",
//             IsActive = true,
//             Description = "This is a description" 
//         };

//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.ActivateRoleService(id));
//         Assert.Equal((int)HttpStatusCode.BadRequest, exception.StatusCode);
//         Assert.Equal("Role is activated.", exception.Message);
//     }

//     [Fact]
//     public async Task ActivateRoleService_ThrowsException_WhenActivationFails()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role
//         {
//             Id = id,
//             Name = "Admin",
//             IsActive = false,
//             Description = "This is a description" 
//         };

//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);
//         _roleManagerMock.Setup(rm => rm.UpdateAsync(role)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.ActivateRoleService(id));
//         Assert.Equal((int)HttpStatusCode.BadRequest, exception.StatusCode);
//         Assert.Equal("Failed to activate user.", exception.Message);
//     }


//     [Fact]
//     public async Task DeleteRoleService_DeletesRoleSuccessfully()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         var role = new Role 
//         { 
//             Id = id, 
//             Name = "Admin", 
//             IsActive = true,
//             Description = "This is a description" 
//         };

//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync(role);
//         _roleManagerMock.Setup(rm => rm.DeleteAsync(role)).ReturnsAsync(IdentityResult.Success);

//         // Act
//         var result = await _roleService.DeleteRoleService(id);

//         // Assert
//         Assert.True(result.Success);
//         Assert.Contains("Role Admin deleted successfully", result.Data);
//     }

//     [Fact]
//     public async Task DeleteRoleService_ThrowsException_WhenRoleNotFound()
//     {
//         // Arrange
//         var id = Guid.NewGuid().ToString();
//         _roleManagerMock.Setup(rm => rm.FindByIdAsync(id)).ReturnsAsync((Role)null!);

//         // Act & Assert
//         var exception = await Assert.ThrowsAsync<CustomException>(() => _roleService.DeleteRoleService(id));
//         Assert.Equal("Role not found.", exception.Message);
//     }
// }