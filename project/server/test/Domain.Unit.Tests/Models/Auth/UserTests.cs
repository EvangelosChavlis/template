// // source
// using server.src.Domain.Models.Auth;

// namespace server.test.Domain.Unit.Tests.Models.Auth;

// public class UserTests
// {
//     [Fact]
//     public void User_ShouldInitializeProperties()
//     {
//         // Arrange & Act
//         var user = new User { UserRoles = new List<UserRole>() }; // Explicitly initialize UserRoles

//         // Assert
//         Assert.Null(user.FirstName); // Default value is null
//         Assert.Null(user.LastName); // Default value is null
//         Assert.Null(user.InitialPassword); // Default value is null
//         Assert.Null(user.Address); // Default value is null
//         Assert.Null(user.ZipCode); // Default value is null
//         Assert.Null(user.City); // Default value is null
//         Assert.Null(user.State); // Default value is null
//         Assert.Null(user.Country); // Default value is null
//         Assert.Null(user.MobilePhoneNumber); // Default value is null
//         Assert.False(user.MobilePhoneNumberConfirmed); // Default value is false
//         Assert.Null(user.Bio); // Default value is null
//         Assert.Equal(DateTime.MinValue, user.DateOfBirth); // Default value is MinValue
//         Assert.False(user.IsActive); // Default value is false
//         Assert.NotNull(user.UserRoles); // UserRoles should not be null
//         Assert.Empty(user.UserRoles); // UserRoles should be an empty list
//     }

//     [Fact]
//     public void User_ShouldSetPropertiesCorrectly()
//     {
//         // Arrange
//         var dateOfBirth = new DateTime(1990, 1, 1);
//         var userRole = new UserRole();
//         var user = new User
//         {
//             FirstName = "John",
//             LastName = "Doe",
//             InitialPassword = "Password123",
//             Address = "123 Main St",
//             ZipCode = "12345",
//             City = "Anytown",
//             State = "Anystate",
//             Country = "USA",
//             MobilePhoneNumber = "555-1234",
//             MobilePhoneNumberConfirmed = true,
//             Bio = "Software Developer",
//             DateOfBirth = dateOfBirth,
//             IsActive = true,
//             UserRoles = new List<UserRole> { userRole }
//         };

//         // Act & Assert
//         Assert.Equal("John", user.FirstName);
//         Assert.Equal("Doe", user.LastName);
//         Assert.Equal("Password123", user.InitialPassword);
//         Assert.Equal("123 Main St", user.Address);
//         Assert.Equal("12345", user.ZipCode);
//         Assert.Equal("Anytown", user.City);
//         Assert.Equal("Anystate", user.State);
//         Assert.Equal("USA", user.Country);
//         Assert.Equal("555-1234", user.MobilePhoneNumber);
//         Assert.True(user.MobilePhoneNumberConfirmed);
//         Assert.Equal("Software Developer", user.Bio);
//         Assert.Equal(dateOfBirth, user.DateOfBirth);
//         Assert.True(user.IsActive);
//         Assert.Single(user.UserRoles);
//         Assert.Contains(userRole, user.UserRoles);
//     }

//     [Fact]
//     public void User_UserRoles_ShouldBeIndependent()
//     {
//         // Arrange
//         var user = new User { UserRoles = new List<UserRole>() };

//         // Act
//         user.UserRoles.Add(new UserRole());

//         // Assert
//         Assert.NotEmpty(user.UserRoles); // The list should now contain an item
//     }
// }
