using AuthAPI.Abstractions;
using AuthAPI.Configurations;
using AuthAPI.Data;
using MongoDB.Driver;

namespace AuthAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    // TODO: refactor with fluent validation
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

        var jwtSection = configuration.GetRequiredSection("JWT");

        services.AddOptions<IdTokenSettings>()
            .Bind(jwtSection.GetRequiredSection(IdTokenSettings.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Secret), "ID Token secret was not specified.")
            .Validate(o => o.ExpirationPeriodInMinutes > 0, "Invalid ID Token expiration period.")
            .Validate(o => o.Secret.Length >= 16, "ID Token secret length must be at least 16 characters long.")
            .ValidateOnStart();

        services.AddOptions<RefreshTokenSettings>()
            .Bind(jwtSection.GetRequiredSection(RefreshTokenSettings.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Secret), "Refresh Token secret was not specified.")
            .Validate(o => o.ExpirationPeriodInDays > 0, "Invalid Refresh Token expiration period.")
            .Validate(o => o.Secret.Length >= 16, "Refresh Token length must be at least 16 characters long.")
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddMongoDbClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddScoped<IMongoClient>(_ =>
            new MongoClient(configuration.GetConnectionString("MongoDbServer")));
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserRepository, UserRepository>();
    }
}