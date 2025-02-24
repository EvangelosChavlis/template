// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles;
using server.src.Application.Auth.UserLogins;
using server.src.Application.Auth.UserLogouts;
using server.src.Application.Auth.UserRoles;
using server.src.Application.Auth.Users;

namespace server.src.Application.Auth;

public static class DI
{
    public static IServiceCollection RegisterAuth(this IServiceCollection services)
    {
        services.RegisterRoles();
        services.RegisterUsers();
        services.RegisterUserRoles();
        services.RegisterUserLogins();
        services.RegisterUserLogouts();

        return services;
    }
}