using SessionAPI.Abstractions;
using SessionAPI.Configurations;
using SessionAPI.Data;
using SessionAPI.Services;

namespace SessionAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<MongoDbConnectionSettings>()
            .Bind(configuration.GetRequiredSection(MongoDbConnectionSettings.SectionName))
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.DatabaseName),
                "Database Name was not specified.")
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.UsersCollectionName),
                "Users collection name was not specified.")
            .ValidateOnStart();

        return services;
    }

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