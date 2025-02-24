// source
using server.src.Api;
using server.src.Api.Helpers;
using server.src.Api.Middlewares;
using server.src.Application;
using server.src.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Application Layer Services
builder.Services.RegisterApplication();

// Register Persistence Layer Services
builder.Services.RegisterPersistence(builder.Configuration);

// Register Api Layer Services
builder.Services.RegisterApi(builder.Logging, builder.WebHost, 
    builder.Environment);

var app = builder.Build();

// Use the custom WebApi middleware
app.UseWebApiMiddleware(app.Environment);

// Run Database Migrations
await DatabaseMigrationHelper.ApplyMigrationsAsync(app.Services);

app.Run();

public partial class Program { }
