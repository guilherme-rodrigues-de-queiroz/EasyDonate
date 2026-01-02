using Scalar.AspNetCore;

namespace EasyDonate.API.Extensions;

public static class ScalarExtensions
{
    public static IApplicationBuilder ConfigureScalarUI(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
            return app;

        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.WithTitle("EasyDonate API")
                   .WithTheme(ScalarTheme.Default)
                   .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        return app;
    }
}
