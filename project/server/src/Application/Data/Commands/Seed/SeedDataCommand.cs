// packages
using System.Net;
using Bogus;

// source
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.Forecasts.Interfaces;
using server.src.Application.Weather.Collections.Warnings.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Interfaces;
using server.src.Application.Data.Interfaces;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Common.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Geography.Natural.Locations.Models;

namespace server.src.Application.Data.Commands;

public record SeedDataCommand() : IRequest<Response<string>>;

public class SeedDataHandler : IRequestHandler<SeedDataCommand, Response<string>>
{
    private readonly SeedDataSettings _seedDataSettings;
    private readonly ICommonRepository _commonRepository;
    private readonly IRoleCommands _roleCommands;
    private readonly IUserCommands _userCommands;
    private readonly IWarningCommands _warningCommands;
    private readonly IForecastCommands _forecastCommands;
    private readonly ICommonQueries _commonQueries;

    private readonly IDataCommands _dataCommands;
    
    private readonly IContinentCommands _continentCommands;
    
    public SeedDataHandler(
        SeedDataSettings seedDataSettings,
        ICommonRepository commonRepository, 
        IRoleCommands roleCommands, 
        IUserCommands userCommands, 
        IWarningCommands warningCommands, 
        IForecastCommands forecastCommands, 
        ICommonQueries commonQueries,
        IContinentCommands continentCommands,

        IDataCommands dataCommands
    )
    {
        _seedDataSettings = seedDataSettings;
        _commonRepository = commonRepository;
        _roleCommands = roleCommands;
        _userCommands = userCommands;
        _warningCommands = warningCommands;
        _forecastCommands = forecastCommands;
        _commonQueries = commonQueries;

        _continentCommands = continentCommands;

        _dataCommands = dataCommands;
    }

    public async Task<Response<string>> Handle(SeedDataCommand command, CancellationToken token = default)
    {
        var faker = new Faker();
        var random = new Random();

        var failures = new List<string>();

        var timezonesExist = await _commonRepository.AnyExistsAsync<Timezone>(token: token);
        if(!timezonesExist)
        {
            var timezonesResponse = await _dataCommands.SeedTimezonesAsync(_seedDataSettings.NaturalPath, token);
            if (!timezonesResponse.Success)
                return new Response<string>()
                    .WithMessage(timezonesResponse.Message!)
                    .WithSuccess(timezonesResponse.Success)
                    .WithFailures(timezonesResponse.Failures)
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithData("Data seeding was failed!");
            failures.AddRange(timezonesResponse.Failures);
            Console.WriteLine("Time zones seeded with failures: " + timezonesResponse.Failures.Count);
        }

        var climateZonesExist = await _commonRepository.AnyExistsAsync<ClimateZone>(token: token);
        if(!climateZonesExist)
        {
            var climateZonesResponse = await _dataCommands.SeedClimateZonesAsync(_seedDataSettings.NaturalPath, token);
            if (!climateZonesResponse.Success)
                return new Response<string>()
                    .WithMessage(climateZonesResponse.Message!)
                    .WithSuccess(climateZonesResponse.Success)
                    .WithFailures(climateZonesResponse.Failures)
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithData("Data seeding was failed!");
            failures.AddRange(climateZonesResponse.Failures);
            Console.WriteLine("Climate zones seeded with failures: " + climateZonesResponse.Failures.Count);
        }

        var surfaceTypesExist = await _commonRepository.AnyExistsAsync<SurfaceType>(token: token);
        if(!surfaceTypesExist)
        {
            var surfaceTypesResponse = await _dataCommands.SeedSurfaceTypeAsync(_seedDataSettings.NaturalPath, token);
            if (!surfaceTypesResponse.Success)
                return new Response<string>()
                    .WithMessage(surfaceTypesResponse.Message!)
                    .WithSuccess(surfaceTypesResponse.Success)
                    .WithFailures(surfaceTypesResponse.Failures)
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithData("Data seeding was failed!");
            failures.AddRange(surfaceTypesResponse.Failures);
            Console.WriteLine("Surface types seeded with failures: " + surfaceTypesResponse.Failures.Count);
        }

        var naturalFeaturesExist = await _commonRepository.AnyExistsAsync<NaturalFeature>(token: token);
        if(!naturalFeaturesExist)
        {
            var naturalFeaturesResponse = await _dataCommands.SeedNaturalFeatureAsync(_seedDataSettings.NaturalPath, token);
            if (!naturalFeaturesResponse.Success)
                return new Response<string>()
                    .WithMessage(naturalFeaturesResponse.Message!)
                    .WithSuccess(naturalFeaturesResponse.Success)
                    .WithFailures(naturalFeaturesResponse.Failures)
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithData("Data seeding was failed!");
            failures.AddRange(naturalFeaturesResponse.Failures);
            Console.WriteLine("Natural features seeded with failures: " + naturalFeaturesResponse.Failures.Count);
        }

        var locationsExist = await _commonRepository.AnyExistsAsync<Location>(token: token);
        if(!locationsExist)
        {
            var locationsResponse = await _dataCommands.SeedLocationsAsync(token);
            if (!locationsResponse.Success)
                return new Response<string>()
                    .WithMessage(locationsResponse.Message!)
                    .WithSuccess(locationsResponse.Success)
                    .WithFailures(locationsResponse.Failures)
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithData("Data seeding was failed!");
            failures.AddRange(locationsResponse.Failures);
            Console.WriteLine("Locations seeded with failures: " + locationsResponse.Failures.Count);
        }
        
        
        // var continentsExist = await _commonRepository.AnyExistsAsync<Continent>(token: token);
        // if(!continentsExist)
        // {
        //     var continentsResponse = await _dataCommands.SeedContinentsAsync(_seedDataSettings.ContinentsPath, token);
        //     if (!continentsResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(continentsResponse.Message!)
        //             .WithSuccess(continentsResponse.Success)
        //             .WithFailures(continentsResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(continentsResponse.Failures);
        // }

        // var countriesExist = await _commonRepository.AnyExistsAsync<Country>(token: token);
        // if(!countriesExist)
        // {
        //     var countriesResponse = await _dataCommands.SeedCountriesAsync(_seedDataSettings.CountriesPath, token);
        //     if (!countriesResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(countriesResponse.Message!)
        //             .WithSuccess(countriesResponse.Success)
        //             .WithFailures(countriesResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(countriesResponse.Failures);
        // }

        // var statesExist = await _commonRepository.AnyExistsAsync<State>(token: token);
        // if(!statesExist)
        // {
        //     var statesResponse = await _dataCommands.SeedStatesAsync(_seedDataSettings.StatesPath, token);
        //     if (!statesResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(statesResponse.Message!)
        //             .WithSuccess(statesResponse.Success)
        //             .WithFailures(statesResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(statesResponse.Failures);
        // }

        // var regionsExist = await _commonRepository.AnyExistsAsync<Region>(token: token);
        // if(!regionsExist)
        // {
        //     var regionsResponse = await _dataCommands.SeedRegionsAsync(_seedDataSettings.RegionsPath, token);
        //     if (!regionsResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(regionsResponse.Message!)
        //             .WithSuccess(regionsResponse.Success)
        //             .WithFailures(regionsResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(regionsResponse.Failures);
        // }

        // var municipalitiesExist = await _commonRepository.AnyExistsAsync<Municipality>(token: token);
        // if(!municipalitiesExist)
        // {
        //     var municipalitiesResponse = await _dataCommands.SeedMunicipalitiesAsync(_seedDataSettings.MunicipalitiesPath, token);
        //     if (!municipalitiesResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(municipalitiesResponse.Message!)
        //             .WithSuccess(municipalitiesResponse.Success)
        //             .WithFailures(municipalitiesResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(municipalitiesResponse.Failures);
        // }

        // var districtsExist = await _commonRepository.AnyExistsAsync<District>(token: token);
        // if(!districtsExist)
        // {
        //     var districtsResponse = await _dataCommands.SeedDistrictsAsync(_seedDataSettings.DistrictsPath, token);
        //     if (!districtsResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(districtsResponse.Message!)
        //             .WithSuccess(districtsResponse.Success)
        //             .WithFailures(districtsResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(districtsResponse.Failures);
        // }

        // var neighborhoodsExist = await _commonRepository.AnyExistsAsync<Neighborhood>(token: token);
        // if(!neighborhoodsExist)
        // {
        //     var neighborhoodsResponse = await _dataCommands.SeedNeighborhoodAsync(_seedDataSettings.NeighborhoodsPath, token);
        //     if (!neighborhoodsResponse.Success)
        //         return new Response<string>()
        //             .WithMessage(neighborhoodsResponse.Message!)
        //             .WithSuccess(neighborhoodsResponse.Success)
        //             .WithFailures(neighborhoodsResponse.Failures)
        //             .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //             .WithData("Data seeding was failed!");
        //     failures.AddRange(neighborhoodsResponse.Failures);
        // }

        // var roles = new List<CreateRoleDto>()
        // {
        //     new ( Name: "User", Description: "User description"),
        //     new ( Name: "Manager", Description: "Manager description"),
        //     new ( Name: "Administrator", Description: "Administrator description"),
        //     new ( Name: "Developer", Description: "Developer description"),
        // };

        // var rolesResponse = await _roleCommands.InitializeRolesAsync(roles, token);

        // if (!rolesResponse.Success)
        //     return new Response<string>()
        //         .WithMessage(rolesResponse.Message!)
        //         .WithSuccess(rolesResponse.Success)
        //         .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //         .WithData("Data seeding was failed!");

        

        

        // // Countries initialization logic
        // var countries = new List<CreateCountryDto>()
        // {
        //     new (
        //         Name: "United States",
        //         Description: "A country in North America.",
        //         Code: "US",
        //         Capital: "Washington D.C.",
        //         Population: 331_002_651,
        //         AreaKm2: 9_833_517,
        //         IsActive: true,
        //         ContinentId: continents.FirstOrDefault(c => c.Name == "North America")?.Id ?? Guid.Empty
        //     ),
        //     new (
        //         Name: "Brazil",
        //         Description: "Largest country in South America.",
        //         Code: "BR",
        //         Capital: "Brasília",
        //         Population: 211_049_527,
        //         AreaKm2: 8_515_767,
        //         IsActive: true,
        //         ContinentId: continents.FirstOrDefault(c => c.Name == "South America")?.Id ?? Guid.Empty
        //     ),
        //     new (
        //         Name: "Germany",
        //         Description: "A country in Europe.",
        //         Code: "DE",
        //         Capital: "Berlin",
        //         Population: 83_783_942,
        //         AreaKm2: 357_022,
        //         IsActive: true,
        //         ContinentId: continents.FirstOrDefault(c => c.Name == "Europe")?.Id ?? Guid.Empty
        //     ),
        //     // Add more countries...
        // };
        
        // var admin = new CreateUserDto(
        //     // User Details
        //     FirstName: "Evangelos",
        //     LastName: "Chavlis",
        //     Email: "evangelos.chavlis@mail.com",
        //     UserName: "evangelos.chavlis",
        //     Password: "Ar@g0rn1996",

        //     // Contact Info
        //     Address: "Mesogeion 235 Avenue",
        //     ZipCode: "154 51",
        //     City: "Athens",
        //     State: "Attica",
        //     Country: "Greece",
        //     PhoneNumber: "+306943567654",
        //     MobilePhoneNumber: "+2105896543",

        //     // Additional Info
        //     Bio: faker.Lorem.Paragraph(),                
        //     DateOfBirth: faker.Date.Past(30, DateTime.Today.AddYears(-18)).ToUniversalTime()
        // );

        // var adminRegistrationResult = await _userCommands.RegisterUserAsync(admin, false, token);
        // if (!adminRegistrationResult.Success)
        //     return new Response<string>()
        //         .WithMessage(adminRegistrationResult.Message!)
        //         .WithSuccess(adminRegistrationResult.Success)
        //         .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //         .WithData("Data seeding was failed!");

        // // Searching Item
        // var filters = new Expression<Func<Role, bool>>[] { r => r.Name!.Equals("Administrator")};
        // var role = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // // Check for existence
        // if (role is null)
        //     return new Response<string>()
        //         .WithMessage("Role not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");

        // // Searching Item
        // var adminUserFilters = new Expression<Func<User, bool>>[] { u => u.UserName!.Equals("evangelos.chavlis") };
        // var adminUser = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // // Check for existence
        // if (adminUser is null)
        //     return new Response<string>()
        //         .WithMessage("User not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");

        // var dtoUsers = new List<CreateUserDto>();

        // for (int i = 0; i < 10; i++)
        // {
        //     var password = await _commonQueries.GeneratePassword(12, token);

        //     var item = new CreateUserDto(
        //         // User Details
        //         FirstName: faker.Name.FirstName(),
        //         LastName: faker.Name.LastName(),
        //         Email: faker.Internet.Email(),
        //         UserName: faker.Internet.UserName(),
        //         Password: password,

        //         // Contact Info
        //         Address: faker.Address.StreetAddress(),
        //         ZipCode: faker.Address.ZipCode(),
        //         City: faker.Address.City(),
        //         State: faker.Address.State(),
        //         Country: faker.Address.Country(),
        //         PhoneNumber: faker.Phone.PhoneNumber(),
        //         MobilePhoneNumber: faker.Phone.PhoneNumber(),

        //         // Additional Info
        //         Bio: faker.Lorem.Paragraph(),                
        //         DateOfBirth: faker.Date.Past(30, DateTime.Today.AddYears(-18)).ToUniversalTime()
        //     );

        //     dtoUsers.Add(item);
        // }

        // var usersResponse = await _userCommands.InitializeUsersAsync(dtoUsers, token);

        // if (!usersResponse.Success)
        //     return new Response<string>()
        //         .WithMessage(usersResponse.Message!)
        //         .WithSuccess(rolesResponse.Success)
        //         .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //         .WithData("Data seeding was failed!");

        // var warningDtos = new List<CreateWarningDto>
        // {
        //     new (
        //         Name: "Extreme", 
        //         Description: "This alert is given for extreme alerts. Immediate action is advised to ensure safety.",
        //         RecommendedActions: "Seek shelter, follow emergency protocols, and stay tuned for official updates."
        //     ),
        //     new (
        //         Name: "High", 
        //         Description: "This alert is given for high alerts. Be prepared for potentially hazardous conditions.",
        //         RecommendedActions: "Stay alert, prepare emergency supplies, and monitor updates from authorities."
        //     ),
        //     new (
        //         Name: "Normal", 
        //         Description: "This alert is given for normal alerts. General conditions are safe, but stay informed.",
        //         RecommendedActions: "No immediate action needed. Stay updated with weather reports."
        //     ), 
        //     new (
        //         Name: "Low", 
        //         Description: "This alert is given for low alerts. Minimal risk; no immediate action required.",
        //         RecommendedActions: "Enjoy normal conditions, but remain aware of potential changes."
        //     )
        // };

        // var warningsResponse = await _warningCommands.InitializeWarningsAsync(warningDtos, token);

        // if (!warningsResponse.Success)
        //     return new Response<string>()
        //         .WithMessage("Error in warning seeding")
        //         .WithSuccess(rolesResponse.Success)
        //         .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //         .WithData("Data seeding was failed!");

        // // Searching Items
        // var lowFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Low") };
        // var low = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // // Check for existence
        // if (low is null)
        //     return new Response<string>()
        //         .WithMessage("Low warning not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");


        // var normalFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Normal") };
        // var normal = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // // Check for existence
        // if (normal is null)
        //     return new Response<string>()
        //         .WithMessage("Normal warning not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");

        // var highFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("High") };
        // var high = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // // Check for existence
        // if (high is null)
        //     return new Response<string>()
        //         .WithMessage("High warning not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");

        // var extremeFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Extreme") };
        // var extreme = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // // Check for existence
        // if (extreme is null)
        //     return new Response<string>()
        //         .WithMessage("Extreme warning not found.")
        //         .WithStatusCode((int)HttpStatusCode.NotFound)
        //         .WithSuccess(false)
        //         .WithData("Data seeding was failed!");


        // var summariesBySeason = new Dictionary<string, string[]>
        // {
        //     { "Winter", new[] { "Snowy", "Cloudy", "Windy", "Cold" } },
        //     { "Spring", new[] { "Sunny", "Rainy", "Windy", "Cloudy" } },
        //     { "Summer", new[] { "Sunny", "Hot", "Stormy", "Cloudy" } },
        //     { "Fall", new[] { "Cloudy", "Rainy", "Windy", "Cold" } }
        // };

        
        // var forecastDtos = new List<CreateForecastDto>();

        // for (int i = 0; i < 365; i++)
        // {
        //     var date = DateTime.Now.Date.AddDays(i);
        //     var month = date.Month;

        //     string season;
        //     int minTemp, maxTemp;

        //     // Determine season and temperature range
        //     if (month == 12 || month == 1 || month == 2)
        //     {
        //         season = "Winter";
        //         minTemp = -10;
        //         maxTemp = 5;
        //     }
        //     else if (month >= 3 && month <= 5)
        //     {
        //         season = "Spring";
        //         minTemp = 5;
        //         maxTemp = 20;
        //     }
        //     else if (month >= 6 && month <= 8)
        //     {
        //         season = "Summer";
        //         minTemp = 20;
        //         maxTemp = 35;
        //     }
        //     else
        //     {
        //         season = "Fall";
        //         minTemp = 5;
        //         maxTemp = 20;
        //     }

        //     // Generate random temperature within range
        //     var temperature = random.Next(minTemp, maxTemp + 1);

        //     // Get random summary based on season
        //     var summary = summariesBySeason[season][random.Next(summariesBySeason[season].Length)];

        //     // Determine warning ID
        //     Guid warningId;
        //     if (temperature < 0)
        //         warningId = low.Id;
        //     else if (temperature >= 0 && temperature < 15)
        //         warningId = normal.Id;
        //     else if (temperature >= 15 && temperature < 25)
        //         warningId = high.Id;
        //     else
        //         warningId = extreme.Id;

        //     // // Add forecast to the list
        //     // forecastDtos.Add(new CreateForecastDto(
        //     //     Date: date.ToUniversalTime(),
        //     //     TemperatureC: temperature,
        //     //     Summary: summary,
        //     //     WarningId: warningId
        //     // ));
        // }


        // var forecastsResponse = await _forecastCommands.InitializeForecastsAsync(forecastDtos, token);

        // if (!forecastsResponse.Success)
        //     return new Response<string>()
        //         .WithMessage("Error in forecasts seeding")
        //         .WithSuccess(rolesResponse.Success)
        //         .WithStatusCode((int)HttpStatusCode.InternalServerError)
        //         .WithData("Data seeding was failed!");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success in data seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(failures)
            .WithData("Data seeding was successful!");
    }

}
    