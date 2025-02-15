// packages
using System.Linq.Expressions;
using System.Net;
using Bogus;

// source
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Helpers;
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Weather.Warnings.Dtos;
using server.src.Domain.Weather.Warnings.Models;
using server.src.Domain.Weather.Forecasts.Dtos;

namespace server.src.Application.Data.Commands;

public record SeedDataCommand() : IRequest<Response<string>>;

public class SeedDataHandler : IRequestHandler<SeedDataCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IRoleCommands _roleCommands;
    private readonly IUserCommands _userCommands;
    private readonly IWarningCommands _warningCommands;
    private readonly IForecastCommands _forecastCommands;
    private readonly ICommonQueries _commonQueries; 
        
    
    public SeedDataHandler(
        ICommonRepository commonRepository, 
        IRoleCommands roleCommands, 
        IUserCommands userCommands, 
        IWarningCommands warningCommands, 
        IForecastCommands forecastCommands, 
        ICommonQueries commonQueries
    )
    {   
        _commonRepository = commonRepository;
        _roleCommands = roleCommands;
        _userCommands = userCommands;
        _warningCommands = warningCommands;
        _forecastCommands = forecastCommands;
        _commonQueries = commonQueries;
    }


    public async Task<Response<string>> Handle(SeedDataCommand command, CancellationToken token = default)
    {
        var faker = new Faker();
        var random = new Random();

        var roles = new List<CreateRoleDto>()
        {
            new ( Name: "User", Description: "User description"),
            new ( Name: "Manager", Description: "Manager description"),
            new ( Name: "Administrator", Description: "Administrator description"),
            new ( Name: "Developer", Description: "Developer description"),
        };

        var rolesResponse = await _roleCommands.InitializeRolesAsync(roles, token);

        if (!rolesResponse.Success)
            return new Response<string>()
                .WithMessage(rolesResponse.Message!)
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");
        
        var admin = new CreateUserDto(
            // User Details
            FirstName: "Evangelos",
            LastName: "Chavlis",
            Email: "evangelos.chavlis@mail.com",
            UserName: "evangelos.chavlis",
            Password: "Ar@g0rn1996",

            // Contact Info
            Address: "Mesogeion 235 Avenue",
            ZipCode: "154 51",
            City: "Athens",
            State: "Attica",
            Country: "Greece",
            PhoneNumber: "+306943567654",
            MobilePhoneNumber: "+2105896543",

            // Additional Info
            Bio: faker.Lorem.Paragraph(),                
            DateOfBirth: faker.Date.Past(30, DateTime.Today.AddYears(-18)).ToUniversalTime()
        );

        var adminRegistrationResult = await _userCommands.RegisterUserAsync(admin, false, token);
        if (!adminRegistrationResult.Success)
            return new Response<string>()
                .WithMessage(adminRegistrationResult.Message!)
                .WithSuccess(adminRegistrationResult.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        // Searching Item
        var filters = new Expression<Func<Role, bool>>[] { r => r.Name!.Equals("Administrator")};
        var role = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (role is null)
            return new Response<string>()
                .WithMessage("Role not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");

        // Searching Item
        var adminUserFilters = new Expression<Func<User, bool>>[] { u => u.UserName!.Equals("evangelos.chavlis") };
        var adminUser = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (adminUser is null)
            return new Response<string>()
                .WithMessage("User not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");

        var dtoUsers = new List<CreateUserDto>();

        for (int i = 0; i < 10; i++)
        {
            var password = await _commonQueries.GeneratePassword(12, token);

            var item = new CreateUserDto(
                // User Details
                FirstName: faker.Name.FirstName(),
                LastName: faker.Name.LastName(),
                Email: faker.Internet.Email(),
                UserName: faker.Internet.UserName(),
                Password: password,

                // Contact Info
                Address: faker.Address.StreetAddress(),
                ZipCode: faker.Address.ZipCode(),
                City: faker.Address.City(),
                State: faker.Address.State(),
                Country: faker.Address.Country(),
                PhoneNumber: faker.Phone.PhoneNumber(),
                MobilePhoneNumber: faker.Phone.PhoneNumber(),

                // Additional Info
                Bio: faker.Lorem.Paragraph(),                
                DateOfBirth: faker.Date.Past(30, DateTime.Today.AddYears(-18)).ToUniversalTime()
            );

            dtoUsers.Add(item);
        }

        var usersResponse = await _userCommands.InitializeUsersAsync(dtoUsers, token);

        if (!usersResponse.Success)
            return new Response<string>()
                .WithMessage(usersResponse.Message!)
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        var warningDtos = new List<CreateWarningDto>
        {
            new (
                Name: "Extreme", 
                Description: "This alert is given for extreme alerts. Immediate action is advised to ensure safety.",
                RecommendedActions: "Seek shelter, follow emergency protocols, and stay tuned for official updates."
            ),
            new (
                Name: "High", 
                Description: "This alert is given for high alerts. Be prepared for potentially hazardous conditions.",
                RecommendedActions: "Stay alert, prepare emergency supplies, and monitor updates from authorities."
            ),
            new (
                Name: "Normal", 
                Description: "This alert is given for normal alerts. General conditions are safe, but stay informed.",
                RecommendedActions: "No immediate action needed. Stay updated with weather reports."
            ), 
            new (
                Name: "Low", 
                Description: "This alert is given for low alerts. Minimal risk; no immediate action required.",
                RecommendedActions: "Enjoy normal conditions, but remain aware of potential changes."
            )
        };

        var warningsResponse = await _warningCommands.InitializeWarningsAsync(warningDtos, token);

        if (!warningsResponse.Success)
            return new Response<string>()
                .WithMessage("Error in warning seeding")
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        // Searching Items
        var lowFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Low") };
        var low = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // Check for existence
        if (low is null)
            return new Response<string>()
                .WithMessage("Low warning not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");


        var normalFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Normal") };
        var normal = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // Check for existence
        if (normal is null)
            return new Response<string>()
                .WithMessage("Normal warning not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");

        var highFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("High") };
        var high = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // Check for existence
        if (high is null)
            return new Response<string>()
                .WithMessage("High warning not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");

        var extremeFilters = new Expression<Func<Warning, bool>>[] { w => w.Name.Equals("Extreme") };
        var extreme = await _commonRepository.GetResultByIdAsync(lowFilters, token: token);

        // Check for existence
        if (extreme is null)
            return new Response<string>()
                .WithMessage("Extreme warning not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Data seeding was failed!");


        var summariesBySeason = new Dictionary<string, string[]>
        {
            { "Winter", new[] { "Snowy", "Cloudy", "Windy", "Cold" } },
            { "Spring", new[] { "Sunny", "Rainy", "Windy", "Cloudy" } },
            { "Summer", new[] { "Sunny", "Hot", "Stormy", "Cloudy" } },
            { "Fall", new[] { "Cloudy", "Rainy", "Windy", "Cold" } }
        };

        
        var forecastDtos = new List<CreateForecastDto>();

        for (int i = 0; i < 365; i++)
        {
            var date = DateTime.Now.Date.AddDays(i);
            var month = date.Month;

            string season;
            int minTemp, maxTemp;

            // Determine season and temperature range
            if (month == 12 || month == 1 || month == 2)
            {
                season = "Winter";
                minTemp = -10;
                maxTemp = 5;
            }
            else if (month >= 3 && month <= 5)
            {
                season = "Spring";
                minTemp = 5;
                maxTemp = 20;
            }
            else if (month >= 6 && month <= 8)
            {
                season = "Summer";
                minTemp = 20;
                maxTemp = 35;
            }
            else
            {
                season = "Fall";
                minTemp = 5;
                maxTemp = 20;
            }

            // Generate random temperature within range
            var temperature = random.Next(minTemp, maxTemp + 1);

            // Get random summary based on season
            var summary = summariesBySeason[season][random.Next(summariesBySeason[season].Length)];

            // Determine warning ID
            Guid warningId;
            if (temperature < 0)
                warningId = low.Id;
            else if (temperature >= 0 && temperature < 15)
                warningId = normal.Id;
            else if (temperature >= 15 && temperature < 25)
                warningId = high.Id;
            else
                warningId = extreme.Id;

            // Add forecast to the list
            forecastDtos.Add(new CreateForecastDto(
                Date: date.ToUniversalTime(),
                TemperatureC: temperature,
                Summary: summary,
                WarningId: warningId
            ));
        }


        var forecastsResponse = await _forecastCommands.InitializeForecastsAsync(forecastDtos, token);

        if (!forecastsResponse.Success)
            return new Response<string>()
                .WithMessage("Error in forecasts seeding")
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success in data seeding")
            .WithSuccess(true)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Data seeding was successful!");
    }

}
    