// packages
using AspNetCoreRateLimit;

namespace server.src.Api.Extensions;

public static class RateLimitExtension
{
    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        // Bind options from appsettings.json
        services.AddOptions();

        // Configure IP Rate Limit options
        services.Configure<IpRateLimitOptions>(services.BuildServiceProvider()
            .GetRequiredService<IConfiguration>().GetSection("IpRateLimiting"));

        // Configure Client Rate Limit options
        services.Configure<ClientRateLimitOptions>(services.BuildServiceProvider()
            .GetRequiredService<IConfiguration>().GetSection("RateLimiting"));

        // Add necessary services
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }
}