namespace EasyDonate.API.Extensions;

public static class CorsPolicyExtensions
{
    public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(opt =>
        {
            opt.AddPolicy("Development", policy =>
            {
                policy.WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:4200",
                        "http://localhost:5173",
                        "http://localhost:8080"
                    )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });

            opt.AddPolicy("Production", policy =>
            {
                if (allowedOrigins.Length == 0)
                    throw new InvalidOperationException("Nenhuma origem configurada para CORS em produção.");

                policy.WithOrigins(allowedOrigins)
                    .WithMethods("GET", "POST", "PATCH")
                    .WithHeaders("Content-Type", "Authorization")
                    .AllowCredentials()
                    .SetPreflightMaxAge(TimeSpan.FromHours(1));
            });
        });
    }
}
