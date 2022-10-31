using Bismuth.Crypto.Abstractions;
using Bismuth.Crypto.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bismuth.Crypto;

public static class DependencyInjection
{
    public static IServiceCollection AddBismuthCrypto(this IServiceCollection services)
    {
        return services
            .AddScoped<IPasswordHashService, PasswordHashService>()
            .AddScoped<IJwtService, JwtService>();
    }
}