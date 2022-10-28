using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Web.Bismuth.Configurations;
using Web.Bismuth.Infrastructure;
using static GrpcAuthApi.AuthApi;
using static GrpcUserApi.UserApi;

namespace Web.Bismuth.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<UrlsConfiguration>()
            .Bind(configuration.GetSection(UrlsConfiguration.SectionName))
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.AuthAPI),
                "Auth API gRPC server URL was not specified."
            )
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.UserAPI),
                "User API gRPC server URL was not specified.")
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

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddFluentValidationRulesToSwagger()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bismuth Web App",
                    Version = "v1"
                });
            });
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddScoped<GrpcExceptionInterceptor>();

        services.AddGrpcClient<UserApiClient>((services, options) =>
        {
            var userApiUrl = services.GetRequiredService<IOptions<UrlsConfiguration>>()
                .Value.UserAPI;
            options.Address = new Uri(userApiUrl);
        }).AddInterceptor<GrpcExceptionInterceptor>();

        services.AddGrpcClient<AuthApiClient>((services, options) =>
        {
            var authApiUrl = services.GetRequiredService<IOptions<UrlsConfiguration>>()
                .Value.AuthAPI;
            options.Address = new Uri(authApiUrl);
        }).AddInterceptor<GrpcExceptionInterceptor>();

        return services;
    }
}