// // source
// using server.src.Domain.Models.Common;

// namespace server.tests.Domain.Models.Common;

// public class EnvelopeTests
// {
//     // Test for a simple type, e.g., string
//     public class TestClass
//     {
//         public string Name { get; set; }
//     }

//     [Fact]
//     public void Envelope_Constructor_ShouldInitializeProperties()
//     {
//         // Arrange & Act
//         var envelope = new Envelope<TestClass>();

//         // Assert
//         Assert.Null(envelope.Rows); // By default, Rows should be null
//         Assert.Null(envelope.UrlQuery); // By default, UrlQuery should be null
//     }

//     [Fact]
//     public void Envelope_SetRows_ShouldAssignValuesCorrectly()
//     {
//         // Arrange
//         var envelope = new Envelope<TestClass>
//         {
//             Rows =
//             [
//                 new() { Name = "John" },
//                 new() { Name = "Doe" }
//             ]
//         };

//         // Act & Assert
//         Assert.NotNull(envelope.Rows);
//         Assert.Equal(2, envelope.Rows.Count);
//         Assert.Equal("John", envelope.Rows[0].Name);
//         Assert.Equal("Doe", envelope.Rows[1].Name);
//     }

//     [Fact]
//     public void Envelope_SetUrlQuery_ShouldAssignValuesCorrectly()
//     {
//         // Arrange
//         var urlQuery = new UrlQuery
//         {
//             PageNumber = 2,
//             PageSize = 50,
//             Filter = "Name=John",
//             SortBy = "Age",
//             SortDescending = true,
//             TotalRecords = 100
//         };

//         var envelope = new Envelope<TestClass>
//         {
//             UrlQuery = urlQuery
//         };

//         // Act & Assert
//         Assert.NotNull(envelope.UrlQuery);
//         Assert.Equal(2, envelope.UrlQuery.PageNumber);
//         Assert.Equal(50, envelope.UrlQuery.PageSize);
//         Assert.Equal("Name=John", envelope.UrlQuery.Filter);
//         Assert.Equal("Age", envelope.UrlQuery.SortBy);
//         Assert.True(envelope.UrlQuery.SortDescending);
//         Assert.Equal(100, envelope.UrlQuery.TotalRecords);
//     }

//     [Fact]
//     public void Envelope_WithEmptyRows_ShouldReturnEmptyList()
//     {
//         // Arrange
//         var envelope = new Envelope<TestClass>
//         {
//             Rows = []
//         };

//         // Act & Assert
//         Assert.NotNull(envelope.Rows);
//         Assert.Empty(envelope.Rows); // Should be empty
//     }

//     [Fact]
//     public void Envelope_SetRowsToNull_ShouldHandleNullRowsGracefully()
//     {
//         // Arrange
//         var envelope = new Envelope<TestClass>
//         {
//             Rows = null!
//         };

//         // Act & Assert
//         Assert.Null(envelope.Rows); // Should be null
//     }
// }