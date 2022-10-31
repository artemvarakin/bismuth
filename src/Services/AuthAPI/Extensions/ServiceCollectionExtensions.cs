using AuthAPI.Abstractions;
using AuthAPI.Configurations;
using AuthAPI.Data;
using MongoDB.Driver;

namespace AuthAPI.Extensions;

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