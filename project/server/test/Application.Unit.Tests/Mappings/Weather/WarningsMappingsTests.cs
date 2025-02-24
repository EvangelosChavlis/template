// // source
// using server.src.Application.Mappings.Weather;
// using server.src.Domain.Dto.Weather;
// using server.src.Domain.Extensions;
// using server.src.Domain.Models.Weather;

// namespace server.tests.Application.Mappings.Weather;

// public class WarningsMappingsTests
// {
//     [Fact]
//     public void ListItemWarningDtoMapping_MapsWarningToListItemWarningDto()
//     {
//         // Arrange
//         var warning = new Warning
//         {
//             Id = Guid.NewGuid(),
//             Name = "Heatwave",
//             Description = "Extreme heat expected",
//             Forecasts = new[]
//             {
//                 new Forecast { Date = DateTime.UtcNow, TemperatureC = 35 },
//                 new Forecast { Date = DateTime.UtcNow.AddDays(1), TemperatureC = 37 }
//             }.ToList()
//         };

//         // Act
//         var result = warning.ListItemWarningDtoMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(warning.Id, result.Id);
//         Assert.Equal(warning.Name, result.Name);
//         Assert.Equal(warning.Description, result.Description);
//         Assert.Equal(warning.Forecasts.Count(), result.Count);
//     }

//     [Fact]
//     public void PickerWarningDtoMapping_MapsWarningToPickerWarningDto()
//     {
//         // Arrange
//         var warning = new Warning
//         {
//             Id = Guid.NewGuid(),
//             Name = "Snowstorm",
//             Description = "Heavy snow expected"
//         };

//         // Act
//         var result = warning.PickerWarningDtoMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(warning.Id, result.Id);
//         Assert.Equal(warning.Name, result.Name);
//     }

//     [Fact]
//     public void ItemWarningDtoMapping_MapsWarningToItemWarningDto()
//     {
//         // Arrange
//         var warning = new Warning
//         {
//             Id = Guid.NewGuid(),
//             Name = "Flood Warning",
//             Description = "Flooding expected due to heavy rain",
//             Forecasts = new[]
//             {
//                 new Forecast { Date = DateTime.UtcNow, TemperatureC = 22 },
//                 new Forecast { Date = DateTime.UtcNow.AddDays(1), TemperatureC = 24 }
//             }.ToList()
//         };

//         // Act
//         var result = warning.ItemWarningDtoMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(warning.Id, result.Id);
//         Assert.Equal(warning.Name, result.Name);
//         Assert.Equal(warning.Description, result.Description);
//         Assert.Equal(
//             string.Join(", ", warning.Forecasts.Select(c => $"{c.Date.GetFullLocalDateTimeString()}, {c.TemperatureC}")),
//             string.Join(", ", result.Forecasts)
//         );
//     }

//     [Fact]
//     public void CreateWarningModelMapping_CreatesWarningModelFromDto()
//     {
//         // Arrange
//         var warningDto = new WarningDto(
//             Name: "Hurricane Warning", 
//             Description: "Severe hurricane expected"
//         );

//         // Act
//         var result = warningDto.CreateWarningModelMapping();

//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(warningDto.Name, result.Name);
//         Assert.Equal(warningDto.Description, result.Description);
//     }

//     [Fact]
//     public void UpdateWarningMapping_UpdatesWarningModelFromDto()
//     {
//         // Arrange
//         var warningDto = new WarningDto(
//             Name: "Tornado Warning",
//             Description: "Tornado expected in the area"
//         );
       
//         var warning = new Warning
//         {
//             Id = Guid.NewGuid(),
//             Name = "Old Warning",
//             Description = "Outdated description"
//         };

//         // Act
//         warningDto.UpdateWarningMapping(warning);

//         // Assert
//         Assert.Equal(warningDto.Name, warning.Name);
//         Assert.Equal(warningDto.Description, warning.Description);
//     }
// }