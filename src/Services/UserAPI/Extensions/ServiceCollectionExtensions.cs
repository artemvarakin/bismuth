using System.Reflection;
using Mapster;
using MapsterMapper;
using MongoDB.Driver;
using Serilog;
using UserAPI.Abstractions;
using UserAPI.Configurations;
using UserAPI.Data;
using UserAPI.Infrastructure;

namespace UserAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<MongoDbConnectionSettings>()
            .Bind(configuration.GetRequiredSection(MongoDbConnectionSettings.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.DatabaseName), "Database Name was not specified.")
            .Validate(o => !string.IsNullOrWhiteSpace(o.UsersCollectionName), "Users collection name was not specified.")
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection ConfigureLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            builder
                .ClearProviders()
                .AddSerilog(logger);
        });
    }

    public static IServiceCollection AddExceptionInterceptor(this IServiceCollection services)
    {
        return services.AddGrpc(options =>
            options.Interceptors.Add<ExceptionInterceptor>())
            .Services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserRepository, UserRepository>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddDataMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        return services
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();
    }

    public static IServiceCollection AddMongoDbClient(this IServiceCollection services, IConfiguration configuration)
        => services.AddTransient<IMongoClient>(_ =>
            new MongoClient(configuration.GetConnectionString("MongoDbServer")));
}