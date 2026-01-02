using EasyDonate.Application.Interfaces;
using EasyDonate.Application.Interfaces.Addresses;
using EasyDonate.Application.Interfaces.Appointments;
using EasyDonate.Application.Interfaces.Auth;
using EasyDonate.Application.Interfaces.Donations;
using EasyDonate.Application.Interfaces.Donors;
using EasyDonate.Application.Interfaces.Ongs;
using EasyDonate.Application.Interfaces.Security;
using EasyDonate.Application.Interfaces.Users;
using EasyDonate.Persistence.Context;
using EasyDonate.Persistence.Repositories;
using EasyDonate.Persistence.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyDonate.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySQL");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services
            .AddScoped<IOngRepository, OngRepository>()
            .AddScoped<IDonorRepository, DonorRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAddressRepository, AddressRepository>()
            .AddScoped<IDonationRepository, DonationRepository>()
            .AddScoped<IAppointmentRepository, AppointmentRepository>()
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenService, JwtTokenService>()
            .AddScoped<IUnityOfWork, UnityOfWork>();

        return services;
    }
}