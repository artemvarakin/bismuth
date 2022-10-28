using SessionAPI.Abstractions;
using SessionAPI.Data;
using SessionAPI.Services;

namespace SessionAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IJwtService, JwtService>();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserRepository, UserRepository>();
    }
}