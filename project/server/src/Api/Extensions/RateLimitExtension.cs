// packages
using AspNetCoreRateLimit;

namespace server.src.Api.Extensions;

public static class RateLimitExtension
{
    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new() {
                Endpoint = "*",
                Limit = 100,
                Period = "1m"
            }
        };

        services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = rateLimitRules; });
        
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }
}