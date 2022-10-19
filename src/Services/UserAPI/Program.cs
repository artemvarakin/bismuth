using Bismuth.Crypto;
using FluentValidation;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserAPI.Extensions;
using UserAPI.Grpc;

var builder = WebApplication.CreateBuilder(args);
{
    // Setup a HTTP/2 endpoint without TLS. Only for development.
    builder.WebHost.ConfigureKestrel(options =>
        options.ListenLocalhost(5129, o => o.Protocols = HttpProtocols.Http2));

    builder.Services
        .AddValidatorsFromAssemblyContaining<Program>()
        .AddOptionsConfiguration(builder.Configuration)
        .ConfigureLogging(builder.Configuration)
        .AddMongoDbClient(builder.Configuration)
        .AddExceptionInterceptor()
        .AddBismuthCrypto()
        .AddDataMappings()
        .AddRepositories()
        .AddServices();

    builder.Services.AddGrpc();
}

var app = builder.Build();

app.MapGrpcService<UserManager>();

app.Run();
