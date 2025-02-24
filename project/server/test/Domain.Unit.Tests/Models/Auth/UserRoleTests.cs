// // source
// using server.src.Domain.Models.Auth;

// namespace server.test.Domain.Unit.Tests.Models.Auth;

// public class UserRoleTests
// {
//     [Fact]
//     public void UserRole_ShouldInitializePropertiesToNull()
//     {
//         // Arrange & Act
//         var userRole = new UserRole();

//         // Assert
//         Assert.Null(userRole.User); // User should be null by default
//         Assert.Null(userRole.Role); // Role should be null by default
//         Assert.Null(userRole.UserId); // UserId should be null by default
//         Assert.Null(userRole.RoleId); // RoleId should be null by default
//     }

//     [Fact]
//     public void UserRole_ShouldSetUserPropertyCorrectly()
//     {
//         // Arrange
//         var user = new User
//         {
//             Id = "user1",
//             FirstName = "John",
//             LastName = "Doe"
//         };
//         var userRole = new UserRole { User = user };

//         // Act & Assert
//         Assert.NotNull(userRole.User); // User should not be null
//         Assert.Equal("user1", userRole.User.Id); // User Id should match
//         Assert.Equal("John", userRole.User.FirstName); // User FirstName should match
//         Assert.Equal("Doe", userRole.User.LastName); // User LastName should match
//     }

//     [Fact]
//     public void UserRole_ShouldSetRolePropertyCorrectly()
//     {
//         // Arrange
//         var role = new Role
//         {
//             Id = "role1",
//             Name = "Admin",
//             Description = "Administrator role"
//         };
//         var userRole = new UserRole { Role = role };

//         // Act & Assert
//         Assert.NotNull(userRole.Role); // Role should not be null
//         Assert.Equal("role1", userRole.Role.Id); // Role Id should match
//         Assert.Equal("Admin", userRole.Role.Name); // Role Name should match
//         Assert.Equal("Administrator role", userRole.Role.Description); // Role Description should match
//     }

//     [Fact]
//     public void UserRole_ShouldSetUserIdAndRoleIdCorrectly()
//     {
//         // Arrange
//         var userRole = new UserRole
//         {
//             UserId = "user1",
//             RoleId = "role1"
//         };

//         // Act & Assert
//         Assert.Equal("user1", userRole.UserId); // UserId should match
//         Assert.Equal("role1", userRole.RoleId); // RoleId should match
//     }
// }