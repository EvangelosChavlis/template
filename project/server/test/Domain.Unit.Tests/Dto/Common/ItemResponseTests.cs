// // source
// using server.src.Domain.Dto.Common;

// namespace server.test.Domain.Unit.Tests.Dto.Common;

// public class ItemResponseTests
// {
//     #region Default Behavior Tests

//     [Fact]
//     public void ItemResponse_ShouldInitializeWithNullData()
//     {
//         // Arrange & Act
//         var itemResponse = new ItemResponse<string>();

//         // Assert
//         Assert.Null(itemResponse.Data);
//     }

//     #endregion

//     #region WithData Method Tests

//     [Fact]
//     public void WithData_ShouldSetDataProperty()
//     {
//         // Arrange
//         var itemResponse = new ItemResponse<string>();
//         var testData = "Test data";

//         // Act
//         itemResponse.WithData(testData);

//         // Assert
//         Assert.Equal(testData, itemResponse.Data);
//     }

//     [Fact]
//     public void WithData_ShouldReturnSameObjectForMethodChaining()
//     {
//         // Arrange
//         var itemResponse = new ItemResponse<string>();
//         var testData = "Test data";

//         // Act
//         var result = itemResponse.WithData(testData);

//         // Assert
//         Assert.Same(itemResponse, result);  // Verifies method chaining returns the same object
//     }

//     #endregion
// }