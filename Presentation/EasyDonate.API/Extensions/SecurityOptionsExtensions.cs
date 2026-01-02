using EasyDonate.Persistence.Security;

namespace EasyDonate.API.Extensions;

public static class SecurityOptionsExtensions
{
    public static IServiceCollection ConfigureSecurityOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SecurityOptions>(configuration.GetSection("Security"));
        return services;
    }
}
