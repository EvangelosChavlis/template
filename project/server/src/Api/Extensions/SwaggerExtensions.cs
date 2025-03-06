// packages
using Microsoft.OpenApi.Models;

namespace server.src.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            // Default Swagger Documentation
            c.SwaggerDoc("data", new OpenApiInfo
            {
                Title = "data API",
                Version = "v1"
            });
            c.SwaggerDoc("auth", new OpenApiInfo
            {
                Title = "auth API",
                Version = "v1"
            });
            c.SwaggerDoc("weather", new OpenApiInfo
            {
                Title = "weather API",
                Version = "v1"
            });
            c.SwaggerDoc("geography-administrative", new OpenApiInfo
            {
                Title = "Geography Administrative API",
                Version = "v1"
            });
            c.SwaggerDoc("geography-natural", new OpenApiInfo
            {
                Title = "Geography Natural API",
                Version = "v1"
            });
            c.SwaggerDoc("metrics", new OpenApiInfo
            {
                Title = "metrics API",
                Version = "v1"
            });
            c.SwaggerDoc("errors", new OpenApiInfo
            {
                Title = "errors API",
                Version = "v1"
            });

            // JWT Token Authentication API Documentation
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        

        return services;
    }

    public static void UseCustomSwagger(this IApplicationBuilder app)
    {
        // Configure Swagger UI for each endpoint
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/data/swagger.json", "data API");
            c.SwaggerEndpoint("/swagger/auth/swagger.json", "auth API");
            c.SwaggerEndpoint("/swagger/weather/swagger.json", "weather API");
            c.SwaggerEndpoint(
                "/swagger/geography-administrative/swagger.json", 
                "geography-administrative API"
            );
            c.SwaggerEndpoint(
                "/swagger/geography-natural/swagger.json", 
                "geography-natural API"
            );
            c.SwaggerEndpoint("/swagger/metrics/swagger.json", "metrics API");
            c.RoutePrefix = "swagger";
        });
    }
}