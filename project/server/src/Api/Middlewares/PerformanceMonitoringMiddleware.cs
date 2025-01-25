// packages
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Models.Metrics;
using server.src.Persistence.Contexts;

namespace server.src.Api.Middlewares;

public class PerformanceMonitoringMiddleware
{
    private readonly RequestDelegate _next;

    public PerformanceMonitoringMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Capture memory usage before processing the request
        var initialMemory = GC.GetTotalMemory(true);

        // Capture CPU usage before processing the request
        var initialCpuTime = Process.GetCurrentProcess().TotalProcessorTime;

        // Capture request information
        var method = context.Request.Method;
        var path = context.Request.Path;

        // Capture request body size for relevant HTTP methods
        long requestBodySize = 0;
        if (method == HttpMethods.Post || method == HttpMethods.Put || method == HttpMethods.Patch)
        {
            requestBodySize = await GetRequestBodySize(context.Request);
        }

        // Replace the response body stream with a custom stream to capture the response size
        var originalResponseBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        var requestTimestamp = DateTime.UtcNow;

        await _next(context);

        var responseTimestamp = DateTime.UtcNow;

        // Capture memory usage after processing the request
        var finalMemory = GC.GetTotalMemory(true);
        var memoryUsed = finalMemory - initialMemory;

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        // Capture CPU usage after processing the request
        var finalCpuTime = Process.GetCurrentProcess().TotalProcessorTime;
        var cpuUsage = (finalCpuTime - initialCpuTime).TotalMilliseconds;

        // Capture response size
        var responseBodySize = responseBodyStream.Length;

        // Copy the contents of the new memory stream (response body) to the original stream
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        context.Response.Body = originalResponseBodyStream;

        // Retrieve the userName from claims
        var userName = context.User.FindFirst(ClaimTypes.Name)?.Value;

        // Save the telemetry data to the database
        var scope = context.RequestServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        
        if (user is not null)
        {
            var telemetry = new Telemetry
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                StatusCode = context.Response.StatusCode,
                ResponseTime = elapsedMilliseconds,
                MemoryUsed = memoryUsed,
                CPUusage = cpuUsage,
                RequestBodySize = requestBodySize,
                RequestTimestamp = requestTimestamp,
                ResponseBodySize = responseBodySize,
                ResponseTimestamp = responseTimestamp,
                ClientIp = context.Connection.RemoteIpAddress?.ToString()!,
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                ThreadId = Environment.CurrentManagedThreadId.ToString(),
                UserId = user.Id,
                User = user
            };

            dbContext.TelemetryRecords.Add(telemetry);
            await dbContext.SaveChangesAsync();
        }

    }

    private async Task<long> GetRequestBodySize(HttpRequest request)
    {
        // Enable buffering so the request body can be read multiple times
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();

        // Reset the request body stream position so it can be read again by the next middleware
        request.Body.Position = 0;

        // Return the size of the request body in bytes
        return Encoding.UTF8.GetByteCount(requestBody);
    }
}