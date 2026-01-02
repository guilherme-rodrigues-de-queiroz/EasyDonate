using EasyDonate.Application.Interfaces.Addresses;
using EasyDonate.Application.Interfaces.Auth;
using EasyDonate.Application.Interfaces.Donations;
using EasyDonate.Application.Interfaces.Donors;
using EasyDonate.Application.Interfaces.Ongs;
using EasyDonate.Application.Interfaces.Users;
using EasyDonate.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EasyDonate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, AppDomain.CurrentDomain.GetAssemblies());

        services
            .AddScoped<IOngService, OngService>()
            .AddScoped<IDonorService, DonorService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAddressService, AddressService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IDonationService, DonationService>();

        return services;
    }
}
