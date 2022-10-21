using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Serilog;
using Web.Bismuth.Configurations;
using Web.Bismuth.Infrastructure;
using static GrpcUserApi.UserApi;

namespace Web.Bismuth.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<UrlsConfiguration>()
            .Bind(configuration.GetSection(UrlsConfiguration.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.GrpcUserApi), "User API gRPC server URL was not specified.")
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

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<GrpcExceptionInterceptor>();

        services.AddGrpcClient<UserApiClient>((services, options) =>
        {
            var userApiUrl = services.GetRequiredService<IOptions<UrlsConfiguration>>().Value.GrpcUserApi;
            options.Address = new Uri(userApiUrl);
        }).AddInterceptor<GrpcExceptionInterceptor>();

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
}