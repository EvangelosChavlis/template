// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Auth.Users.Queries;
using server.src.Application.Auth.Users.Commands;
using server.src.Application.Auth.Users.Services;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Users;

public static class DependencyInjection
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        // register query handlers        
        services.AddScoped<IRequestHandler<GetUsersQuery, ListResponse<List<ListItemUserDto>>>, GetUsersHandler>();
        services.AddScoped<IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>>, GetUserByIdHandler>();

        // register queries
        services.AddScoped<IUserQueries, UserQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<InitializeUsersCommand, Response<string>>, InitializeUsersHandler>();
        services.AddScoped<IRequestHandler<RegisterUserCommand, Response<string>>, RegisterUserHandler>();
        services.AddScoped<IRequestHandler<UpdateUserCommand, Response<string>>, UpdateUserHandler>();
        services.AddScoped<IRequestHandler<ForgotPasswordCommand, Response<string>>, ForgotPasswordHandler>();
        services.AddScoped<IRequestHandler<ResetPasswordCommand, Response<string>>, ResetPasswordHandler>();
        services.AddScoped<IRequestHandler<GeneratePasswordCommand, Response<string>>, GeneratePasswordHandler>();
        services.AddScoped<IRequestHandler<Enable2FACommand, Response<string>>, Enable2FAHandler>();
        services.AddScoped<IRequestHandler<Verify2FACommand, Response<string>>, Verify2FAHandler>();
        services.AddScoped<IRequestHandler<RefreshTokenCommand, Response<string>>, RefreshTokenHandler>();
        services.AddScoped<IRequestHandler<ActivateUserCommand, Response<string>>, ActivateUserHandler>();
        services.AddScoped<IRequestHandler<DeactivateUserCommand, Response<string>>, DeactivateUserHandler>();
        services.AddScoped<IRequestHandler<LockUserCommand, Response<string>>, LockUserHandler>();
        services.AddScoped<IRequestHandler<UnlockUserCommand, Response<string>>, UnlockUserHandler>();
        services.AddScoped<IRequestHandler<ConfirmEmailUserCommand, Response<string>>, ConfirmEmailUserHandler>();
        services.AddScoped<IRequestHandler<ConfirmPhoneNumberUserCommand, Response<string>>, ConfirmPhoneNumberUserHandler>();
        services.AddScoped<IRequestHandler<ConfirmMobilePhoneNumberUserCommand, Response<string>>, ConfirmMobilePhoneNumberUserHandler>();
        services.AddScoped<IRequestHandler<DeleteUserCommand, Response<string>>, DeleteUserHandler>();

        // register commands
        services.AddScoped<IUserCommands, UserCommands>();

        return services;
    }
}
