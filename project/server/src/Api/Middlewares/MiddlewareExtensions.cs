namespace server.src.Api.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseWebApiMiddleware(this IApplicationBuilder builder)
    {
        // Add any other WebApi related services here
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
        // builder.UseMiddleware<JwtMiddleware>();


        return builder;
    }
}