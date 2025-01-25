// packages
using AspNetCoreRateLimit;

// source
using server.src.Api.Extensions;

namespace server.src.Api.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseWebApiMiddleware(this IApplicationBuilder builder, IWebHostEnvironment environment)
    {
        // Configure CORS policy
        builder.UseCors("AllowSpecificOrigin");

        // Configure the HTTP request pipeline.
        if (environment.IsDevelopment() || environment.IsProduction())
        {
            builder.UseSwagger();
            builder.UseCustomSwagger();
        }

        // Exception Handling Middleware
        builder.UseMiddleware<ExceptionHandlingMiddleware>();

        // Error Logging Middleware
        builder.UseMiddleware<ErrorLoggingMiddleware>();

        // JWT Middleware for authentication
        builder.UseMiddleware<JwtMiddleware>();

        // Performance Monitoring Middleware
        builder.UseMiddleware<PerformanceMonitoringMiddleware>();

        // Client ID Middleware
        builder.UseMiddleware<ClientIdMiddleware>();

        // Configure the HTTP request pipeline.
        builder.UseMiddleware<SecurityStampValidatorMiddleware>();

        // IP Rate Limiting
        builder.UseIpRateLimiting();

        // Enable ASP.NET Core routing
        builder.UseRouting();

        // Enable Authentication and Authorization
        builder.UseAuthentication();
        builder.UseAuthorization();

        // Map Controllers (API endpoints)
        builder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return builder;
    }
}
