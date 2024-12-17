// using System.Threading.Tasks;
// using Moq;
// using Xunit;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Configuration;
// using server.src.Domain.Models.Auth;
// using server.src.Application.Services.Auth;
// using server.src.Domain.Dto.Auth;
// using System.Collections.Generic;
// using server.src.Persistence.Interfaces;
// using server.src.Persistence.Contexts;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using server.src.Domain.Models.Common;
// using System.Linq.Expressions;
// using MockQueryable.Moq;

// namespace server.test.Application.Unit.Tests.Services.Auth;
// public class UserServiceTests
// {
//     private readonly Mock<UserManager<User>> _userManagerMock;
//     private readonly Mock<SignInManager<User>> _signInManagerMock;
//     private readonly Mock<RoleManager<Role>> _roleManagerMock;
//     private readonly Mock<IConfiguration> _configurationMock;
//     private readonly Mock<ICommonRepository> _commonRepositoryMock;
//     private readonly Mock<DataContext> _dataContextMock;
//     private readonly UserService _userService;

//     public UserServiceTests()
// {
//     _userManagerMock = new Mock<UserManager<User>>(
//         Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
//     );
//     _signInManagerMock = new Mock<SignInManager<User>>(
//         _userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null
//     );
//     _roleManagerMock = new Mock<RoleManager<Role>>(
//         Mock.Of<IRoleStore<Role>>(), null, null, null, null
//     );
//     _configurationMock = new Mock<IConfiguration>();
//     _commonRepositoryMock = new Mock<ICommonRepository>();

//     var options = new DbContextOptionsBuilder<DataContext>()
//         .UseInMemoryDatabase(databaseName: "TestDatabase")
//         .Options;
//     var dataContext = new DataContext(options);

//     _userService = new UserService(
//         _userManagerMock.Object,
//         _signInManagerMock.Object,
//         _roleManagerMock.Object,
//         _configurationMock.Object,
//         dataContext, // Use the in-memory DB context here
//         _commonRepositoryMock.Object
//     );
    
// }

// [Fact]
// public async Task GetUsersService_ShouldReturnUsers_WhenValidInput()
// {
//     // Arrange
//     var pageParams = new UrlQuery
//     {
//         PageNumber = 1,
//         PageSize = 10,
//         SortBy = "UserName",
//         SortDescending = false
//     };

//    var users = new List<User>
//     {
//         new ()
//         {
//             Id = "1",
//             UserName = "john",
//             Email = "john@example.com",
//             FirstName = "John",
//             LastName = "Doe",
//             MobilePhoneNumber = "1234567890",
//             IsActive = true,
//             Bio = "John's bio",
//             DateOfBirth = new DateTime(1990, 1, 1)
//         },
//         new ()
//         {
//             Id = "2",
//             UserName = "doe",
//             Email = "doe@example.com",
//             FirstName = "Jane",
//             LastName = "Doe",
//             MobilePhoneNumber = "0987654321",
//             IsActive = true,
//             Bio = "Jane's bio",
//             DateOfBirth = new DateTime(1992, 2, 2)
//         }
//     };

//     var pagedResult = new Envelope<User>
//     {
//         Rows = users,
//         UrlQuery = pageParams
//     };

//     // Mocking DbSet<User>
//     var mockDbSet = users.AsQueryable().BuildMockDbSet();

//     // Mocking the repository method call
//     _commonRepositoryMock.Setup(repo => repo.GetPagedResultsAsync<User>(
//         mockDbSet.Object,  // Accessing the Object property to get the actual DbSet<User>
//         pageParams,
//         It.IsAny<Expression<Func<User, bool>>[]>(),  // Filter expressions as array
//         It.IsAny<IncludeThenInclude<User>[]>(),  // Include expressions as array
//         It.IsAny<CancellationToken>()
//     )).ReturnsAsync(pagedResult);

//     // Act
//     var result = await _userService.GetUsersService(pageParams);

//     // Assert
//     Assert.NotNull(result);
//     Assert.Equal(2, result.Data!.Count);
//     Assert.Equal(1, result.Pagination!.PageNumber);
//     Assert.Equal(10, result.Pagination.PageSize);
// }



//     [Fact]
// public async Task RegisterUserService_ShouldRegisterUser_WhenValidInput()
// {
//     // Arrange
//     var userDto = new UserDto(
//         FirstName: "John",
//         LastName: "Doe",
//         Email: "johndoe@example.com",
//         UserName: "johndoe",
//         Password: "Password123!",
//         Address: "123 Main St",
//         ZipCode: "12345",
//         City: "Metropolis",
//         State: "NY",
//         Country: "USA",
//         PhoneNumber: "1234567890",
//         MobilePhoneNumber: "0987654321",
//         Bio: "Test Bio",
//         DateOfBirth: new DateTime(1990, 1, 1)
//     );

//     _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);
//     _userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
//     _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
//         .ReturnsAsync(IdentityResult.Success);

//     // Act
//     var result = await _userService.RegisterUserService(userDto, registered: false);

//     // Assert
//     Assert.NotNull(result);
//     Assert.True(result.Success);
//     Assert.Contains("registered successfully", result.Data);
    
//     _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), It.Is<string>(pwd => pwd == "Password123!")), Times.Once);
// }

// }
