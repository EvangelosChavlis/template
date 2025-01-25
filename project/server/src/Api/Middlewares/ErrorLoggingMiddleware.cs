using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Errors;
using server.src.Persistence.Contexts;
using System.Text.Json;

namespace server.src.Api.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorLoggingMiddleware> _logger;

    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var originalResponseBodyStream = context.Response.Body;

        var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body = originalResponseBodyStream;

        if (context.Response.ContentType?.StartsWith("application/json", StringComparison.OrdinalIgnoreCase) == true)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);

            // Console.WriteLine(TryParseResponse(responseText, out var response) && !response.Success);
            if (TryParseResponse(responseText, out var response) && !response.Success)
            {
                var logError = new LogError
                {
                    Id = Guid.NewGuid(),
                    Error = response.Message ?? "An error occurred.",
                    StatusCode = response.StatusCode,
                    Instance = context.TraceIdentifier,
                    ExceptionType = "ServiceFailure",
                    StackTrace = string.Empty,
                    Timestamp = DateTime.UtcNow
                };

                await LogErrorToDatabase(logError, context);

                _logger.LogError("Error logged: {Error}. Path: {Path}, Method: {Method}",
                    logError.Error,
                    context.Request.Path,
                    context.Request.Method);
            }
        }

        await responseBody.CopyToAsync(originalResponseBodyStream);
    }

    private bool TryParseResponse(string responseText, out Response<object> response)
    {
        response = null!;
        try
        {
            response = JsonSerializer.Deserialize<Response<object>>(responseText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return response != null;
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Failed to parse response as Response<object>. Response text: {ResponseText}", responseText);
            return false;
        }
    }

    private async Task LogErrorToDatabase(LogError logError, HttpContext context)
    {
        try
        {
            var scope = context.RequestServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.LogErrors.Add(logError);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to log error to the database.");
        }
    }
}
