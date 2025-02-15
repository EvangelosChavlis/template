// packages
using Newtonsoft.Json;
using server.src.Domain.Common.Models;


// source
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Persistence.Common.Contexts;

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
        var message = exception.Message;

        if (exception is CustomException customEx)
        {
            code = customEx.StatusCode;
            message = customEx.Message;
        }

        var errorDetails = new ErrorDetails
        {
            Error = message,
            StatusCode = code,
            Instance = $"{context.Request.Method} {context.Request.Path}",
            ExceptionType = exception.GetType().Name,
            StackTrace = exception.StackTrace ?? "N/A",
            Timestamp = DateTime.UtcNow
        };

        // Log the exception
        _logger.LogError(exception, "An error occurred: {Error}", exception.Message);

        // Save the error details to the database (optional)
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
            dbContext.MetricsDbSets.LogErrors.Add(logError);
            await dbContext.SaveChangesAsync();
        }

        if (!context.Response.HasStarted)
        {
            var result = JsonConvert.SerializeObject(errorDetails);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            await context.Response.WriteAsync(result);
        }
        else
        {
            _logger.LogWarning("The response has already started, unable to write error response.");
        }
    }

}
