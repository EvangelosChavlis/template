// packages
using Newtonsoft.Json;

// source
using server.src.Domain.Models.Errors;
using server.src.Persistence.Contexts;

namespace server.src.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var errorDetails = new ErrorDetails
        {
            Error = exception.Message,
            StatusCode = code,
            Instance = $"{context.Request.Method} {context.Request.Path}",
            ExceptionType = exception.GetType().Name,
            StackTrace = exception.StackTrace!,
            Timestamp = DateTime.UtcNow
        };

        // Log error
        _logger.LogError(exception, "An error occurred: {Error}", exception.Message);

        // Save error to database using DataContext
        using (var scope = context.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var logError = new LogError
            {
                Error = errorDetails.Error,
                StatusCode = errorDetails.StatusCode,
                Instance = errorDetails.Instance,
                ExceptionType = errorDetails.ExceptionType,
                StackTrace = errorDetails.StackTrace,
                Timestamp = errorDetails.Timestamp
            };
            dbContext.LogErrors.Add(logError);
            await dbContext.SaveChangesAsync();
        }

        // Check if the response has already started
        if (!context.Response.HasStarted)
        {
            // Prepare response
            var result = JsonConvert.SerializeObject(errorDetails);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            await context.Response.WriteAsync(result);
        }
        else
        {
            // If the response has already started, log the issue and avoid modifying the response
            _logger.LogWarning("The response has already started, unable to write error response.");
        }
    }
}


// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
