using System.Text.Json.Serialization;

namespace EasyDonate.API.Extensions;

public static class JsonOptionsExtensions
{
    public static IServiceCollection ConfigureJsonEnumAsString(this IServiceCollection services)
    {
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter()
            );
        });

        return services;
    }
}
