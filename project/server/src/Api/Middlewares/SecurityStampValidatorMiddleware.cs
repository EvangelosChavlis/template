// packages
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Contexts;

namespace server.src.Api.Middlewares;

public class SecurityStampValidatorMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityStampValidatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdString = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tokenSecurityStamp = context.User.FindFirst("SecurityStamp")?.Value;

            if (Guid.TryParse(userIdString, out var userId))
            {
                // Resolve DataContext within a scoped service provider
                using var scope = context.RequestServices.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null || user.SecurityStamp != tokenSecurityStamp)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
        }

        await _next(context);
    }
}
