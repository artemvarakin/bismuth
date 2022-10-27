using System.Reflection;
using Calzolari.Grpc.AspNetCore.Validation;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Bismuth.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddGrpcFluentValidation(this IServiceCollection services)
    {
        services.AddGrpc(options => options.EnableMessageValidation());

        return services
            .AddValidatorsFromAssembly(Assembly.GetCallingAssembly())
            .AddGrpcValidation();
    }

    public static IServiceCollection AddDataMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetCallingAssembly());

        return services
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();
    }
}