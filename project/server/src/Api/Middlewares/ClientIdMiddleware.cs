// packages
using System.Security.Claims;

namespace server.src.Api.Middlewares;

public class ClientIdMiddleware
{
    private readonly RequestDelegate _next;

    public ClientIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Extract user identifier (e.g., user ID or username)
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(userId))
        {
            // Add the user ID as the ClientId to the context
            context.Items["ClientId"] = userId;
        }

        await _next(context);
    }
}