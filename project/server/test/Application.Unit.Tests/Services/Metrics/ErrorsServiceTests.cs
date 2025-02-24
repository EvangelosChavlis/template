// // source
// using server.src.Application.Services.Metrics;
// using server.src.Domain.Exceptions;
// using server.src.Domain.Models.Errors;
// using server.src.Domain.Models.Common;

// // test
// using server.test.Application.Unit.Tests.TestHelpers;

// namespace server.test.Application.Unit.Tests.Services.Metrics;

// public class ErrorsServiceTests : IClassFixture<ContextSetup> 
// {
//     private readonly string _dbName = "ErrorsTestDb";
//     private readonly ContextSetup _contextSetup;

//     public ErrorsServiceTests(ContextSetup contextSetup)
//     {
//         _contextSetup = contextSetup;
//     }

//     [Fact]
//     public async Task GetErrorsService_ShouldReturnPaginatedErrors()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new ErrorQueries(context, commonRepository);

//         // Seed data
//         context.LogErrors.AddRange(new List<LogError>
//         {
//             new()
//             { 
//                 Id = Guid.NewGuid(), 
//                 Error = "Error 1", 
//                 StatusCode = 500, 
//                 Instance = "/api/resource/1",
//                 ExceptionType = "System.Exception",
//                 StackTrace = "at Namespace.Class.Method() in File.cs:line 42",
//                 Timestamp = DateTime.UtcNow 
//             },
//             new()
//             { 
//                 Id = Guid.NewGuid(), 
//                 Error = "Error 2", 
//                 StatusCode = 404, 
//                 Instance = "/api/resource/2",
//                 ExceptionType = "System.NotFoundException",
//                 StackTrace = "at Namespace.Class.AnotherMethod() in AnotherFile.cs:line 24",
//                 Timestamp = DateTime.UtcNow 
//             }
//         });
//         await context.SaveChangesAsync();

//         var query = new UrlQuery
//         {
//             PageSize = 10,
//             PageNumber = 1
//         };

//         // Act
//         var result = await service.GetErrorsService(query);

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(2, result.Data!.Count);
//         Assert.Equal(2, result.Pagination!.TotalRecords);
//     }

//     [Fact]
//     public async Task GetErrorByIdService_ShouldReturnErrorDto()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new ErrorQueries(context, commonRepository);

//         var logError = new LogError
//         {
//             Id = Guid.NewGuid(),
//             Error = "Test Error",
//             StatusCode = 500,
//             Instance = "/test/instance",
//             ExceptionType = "TestException",
//             StackTrace = "Test StackTrace",
//             Timestamp = DateTime.UtcNow
//         };


//         context.LogErrors.Add(logError);
//         await context.SaveChangesAsync();

//         // Act
//         var result = await service.GetErrorByIdService(logError.Id);

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(logError.Id, result.Data!.Id);
//         Assert.Equal(logError.Error, result.Data.Error);
//         Assert.Equal(logError.StatusCode, result.Data.StatusCode);
//     }

//     [Fact]
//     public async Task GetErrorByIdService_ShouldThrowExceptionIfNotFound()
//     {
//         // Arrange
//         var context = _contextSetup.CreateDbContext(_dbName);
//         var commonRepository = _contextSetup.CreateCommonRepository();
//         var service = new ErrorQueries(context, commonRepository);

//         var nonExistentId = Guid.NewGuid();

//         // Act & Assert
//         await Assert.ThrowsAsync<CustomException>(() => service.GetErrorByIdService(nonExistentId));
//     }
// }