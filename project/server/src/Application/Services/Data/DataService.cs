// packages
using System.Net;
using Bogus;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Application.Interfaces.Data;
using server.src.Application.Interfaces.Weather;
using server.src.Application.Services.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Exceptions;
using server.src.Persistence.Contexts;

namespace server.src.Application.Services.Data;

public class DataService : IDataService
{
    private readonly DataContext _context;
    private readonly IWarningsService _warningsService;
    private readonly IForecastsService _forecastsService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    
    public DataService(DataContext context, IWarningsService warningsService, 
        IForecastsService forecastsService, IRoleService roleService, IUserService userService)
    {
        _context = context;
        _warningsService = warningsService;
        _forecastsService = forecastsService;
        _roleService = roleService;
        _userService = userService;  
    }
    
    public async Task<CommandResponse<string>> SeedDataAsync(CancellationToken token = default)
    {
        var faker = new Faker();
        var random = new Random();

        var roles = new List<RoleDto>()
        {
            new ( Name: "User", Description: "User description"),
            new ( Name: "Manager", Description: "Manager description"),
            new ( Name: "Administator", Description: "Administator description")
        }; 

        var rolesResponse = await _roleService.InitializeRolesService(roles);

        var dtoUsers = new List<UserDto>();

        for (int i = 0; i < 10; i++)
        {
            var password = UserService.GeneratePassword(12);

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
                DateOfBirth: faker.Date.Past(30, DateTime.Today.AddYears(-18))
            );

            dtoUsers.Add(item);
        }

        var usersResponse = await _userService.InitializeUsersService(dtoUsers);

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


        var warningsResponse = await _warningsService.InitializeWarningsService(warningDtos, token);

        var low = await _context.Warnings.Where(w => w.Name.Equals("Low")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Low not found", (int)HttpStatusCode.NotFound);

        var normal = await _context.Warnings.Where(w => w.Name.Equals("Normal")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Normal not found", (int)HttpStatusCode.NotFound);

        var high = await _context.Warnings.Where(w => w.Name.Equals("High")).FirstOrDefaultAsync(token) ??
            throw new CustomException("High not found", (int)HttpStatusCode.NotFound);

        var extreme = await _context.Warnings.Where(w => w.Name.Equals("Extreme")).FirstOrDefaultAsync(token) ??
            throw new CustomException("Extreme not found", (int)HttpStatusCode.NotFound);

        var summaries = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Stormy", "Hot", "Cold" };
        var forecastDtos = new List<ForecastDto>();

        // Generate 100 random forecasts
        for (int i = 0; i < 100; i++)
        {
            var temperature = random.Next(-10, 40);

            var summary = summaries[random.Next(summaries.Length)];

            Guid warningId;
            if (temperature < 0)
                warningId = high.Id;
            else if (temperature >= 0 && temperature < 15)
                warningId = low.Id;
            else if (temperature >= 15 && temperature < 25)
                warningId = normal.Id;
            else if (temperature >= 25 && temperature < 35)
                warningId = high.Id;
            else
                warningId = extreme.Id;

            forecastDtos.Add(new ForecastDto(
                Date: DateTime.Now.AddDays(i),
                TemperatureC: temperature,
                Summary: summary,
                WarningId: warningId
            ));
        }

        var forecastsResponse = await _forecastsService.InitializeForecastsService(forecastDtos, token);

        var result = warningsResponse.Success & forecastsResponse.Success & 
            rolesResponse.Success & usersResponse.Success;

         // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData("Data seeding was successful!");
    }

    public async Task<CommandResponse<string>> ClearDataAsync(CancellationToken token = default)
    {   
        _context.Forecasts.RemoveRange(_context.Forecasts);
        _context.Warnings.RemoveRange(_context.Warnings);

        _context.Users.RemoveRange(_context.Users);
        _context.UserRoles.RemoveRange(_context.UserRoles);
        _context.Roles.RemoveRange(_context.Roles);

        _context.TelemetryRecords.RemoveRange(_context.TelemetryRecords);
        _context.LogErrors.RemoveRange(_context.LogErrors);

        // Save changes to delete data from the database
        var result = await _context.SaveChangesAsync(token) > 0;


        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData("Data deleting was successful!");
    }
}