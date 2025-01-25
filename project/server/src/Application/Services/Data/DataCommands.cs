// packages
using System.Net;
using Bogus;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces.Auth.Roles;
using server.src.Application.Interfaces.Auth.UserRoles;
using server.src.Application.Interfaces.Auth.Users;
using server.src.Application.Interfaces.Data;
using server.src.Application.Interfaces.Weather.Forecasts;
using server.src.Application.Interfaces.Weather.Warnings;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Exceptions;
using server.src.Persistence.Contexts;

namespace server.src.Application.Services.Data;

public class DataCommands : IDataCommands
{
    private readonly DataContext _context;
    private readonly IWarningCommands _warningCommands;
    private readonly IForecastCommands _forecastCommands;
    private readonly IRoleCommands _roleCommands;
    private readonly IUserCommands _userCommands;
    private readonly IUserRoleCommands _userRoleCommands;
    private readonly IAuthHelper _authHelper;
    
    public DataCommands(DataContext context, IWarningCommands warningsService, 
        IForecastCommands forecastsService, IRoleCommands roleService, 
        IUserCommands userService, IUserRoleCommands userRoleCommands, IAuthHelper authHelper)
    {
        _context = context;
        _warningCommands = warningsService;
        _forecastCommands = forecastsService;
        _roleCommands = roleService;
        _userCommands = userService;
        _userRoleCommands = userRoleCommands;
        _authHelper = authHelper;  
    }
    
    public async Task<Response<string>> SeedDataAsync(CancellationToken token = default)
    {
        var faker = new Faker();
        var random = new Random();

        var roles = new List<RoleDto>()
        {
            new ( Name: "User", Description: "User description"),
            new ( Name: "Manager", Description: "Manager description"),
            new ( Name: "Administrator", Description: "Administrator description")
        }; 

        var rolesResponse = await _roleCommands.InitializeRolesService(roles, token);

        if (!rolesResponse.Success)
            return new Response<string>()
                .WithMessage(rolesResponse.Message!)
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        var admin = new UserDto(
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

        await _userCommands.RegisterUserService(admin, false, token);
        var roleAdmin = await _context.Roles.Where(r => r.Name!.Equals("Administrator")).FirstAsync(token);
        var userTobeAdmin = await _context.Users.Where(u => u.UserName!.Equals("evangelos.chavlis")).FirstAsync(token);
        await _userRoleCommands.AssignRoleToUserService(userTobeAdmin.Id, roleAdmin.Id, token);

        var dtoUsers = new List<UserDto>();

        for (int i = 0; i < 10; i++)
        {
            var password = _authHelper.GeneratePassword(12);

            var item = new UserDto(
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

        var usersResponse = await _userCommands.InitializeUsersService(dtoUsers, token);

        if (!usersResponse.Success)
            return new Response<string>()
                .WithMessage(usersResponse.Message!)
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");
        
        var warningDtos = new List<WarningDto>
        {
            new ( 
                Name: "Extreme", 
                Description: "This alert is given for extreme alerts. Immediate action is advised to ensure safety." 
            ),
            new ( 
                Name: "High", 
                Description: "This alert is given for high alerts. Be prepared for potentially hazardous conditions." 
            ),
            new ( 
                Name: "Normal", 
                Description: "This alert is given for normal alerts. General conditions are safe, but stay informed." 
            ), 
            new ( 
                Name: "Low", 
                Description: "This alert is given for low alerts. Minimal risk; no immediate action required." 
            )
        };


        var warningsResponse = await _warningCommands.InitializeWarningsService(warningDtos, token);

        if (!warningsResponse.Success)
            return new Response<string>()
                .WithMessage("Error in warning seeding")
                .WithSuccess(rolesResponse.Success)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data seeding was failed!");

        var low = await _context.Warnings.Where(w => w.Name.Equals("Low")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Low not found", (int)HttpStatusCode.NotFound);

        var normal = await _context.Warnings.Where(w => w.Name.Equals("Normal")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Normal not found", (int)HttpStatusCode.NotFound);

        var high = await _context.Warnings.Where(w => w.Name.Equals("High")).FirstOrDefaultAsync(token) ??
            throw new CustomException("High not found", (int)HttpStatusCode.NotFound);

        var extreme = await _context.Warnings.Where(w => w.Name.Equals("Extreme")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Extreme not found", (int)HttpStatusCode.NotFound);

        var summariesBySeason = new Dictionary<string, string[]>
        {
            { "Winter", new[] { "Snowy", "Cloudy", "Windy", "Cold" } },
            { "Spring", new[] { "Sunny", "Rainy", "Windy", "Cloudy" } },
            { "Summer", new[] { "Sunny", "Hot", "Stormy", "Cloudy" } },
            { "Fall", new[] { "Cloudy", "Rainy", "Windy", "Cold" } }
        };

        var forecastDtos = new List<ForecastDto>();

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
            forecastDtos.Add(new ForecastDto(
                Date: date.ToUniversalTime(),
                TemperatureC: temperature,
                Latitude: 0.0,
                Longitude: 0.0,
                Summary: summary,
                WarningId: warningId
            ));
        }


        var forecastsResponse = await _forecastCommands.InitializeForecastsService(forecastDtos, token);

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

    public async Task<Response<string>> ClearDataAsync(CancellationToken token = default)
    {   
        _context.Forecasts.RemoveRange(_context.Forecasts);
        _context.Warnings.RemoveRange(_context.Warnings);

        _context.Users.RemoveRange(_context.Users);
        _context.UserRoles.RemoveRange(_context.UserRoles);
        _context.Roles.RemoveRange(_context.Roles);
        _context.UserClaims.RemoveRange(_context.UserClaims);
        _context.UserLogins.RemoveRange(_context.UserLogins);
        _context.UserLogouts.RemoveRange(_context.UserLogouts);

        _context.TelemetryRecords.RemoveRange(_context.TelemetryRecords);
        _context.LogErrors.RemoveRange(_context.LogErrors);

        // Save changes to delete data from the database
        var result = await _context.SaveChangesAsync(token) > 0;

        if (!result)
            return new Response<string>()
                .WithMessage("Error in clearing data")
                .WithSuccess(result)
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithData("Data clearing was failed!");


        // Initializing object
        return new Response<string>()
            .WithMessage("Success in clearing data")
            .WithSuccess(result)
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithData("Data deleting was successful!");
    }
}