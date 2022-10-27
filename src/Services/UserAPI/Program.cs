using Bismuth.Core;
using Bismuth.Crypto;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserAPI.Extensions;
using UserAPI.Grpc;

var builder = WebApplication.CreateBuilder(args);
{
    // Setup a HTTP/2 endpoint without TLS. Only for development.
    builder.WebHost.ConfigureKestrel(options =>
        options.ListenLocalhost(5129, o => o.Protocols = HttpProtocols.Http2));

    builder.Services
        .AddOptionsConfiguration(builder.Configuration)
        .ConfigureLogging(builder.Configuration)
        .AddMongoDbClient(builder.Configuration)
        .AddMediatR(typeof(Program).Assembly)
        .AddGrpcExceptionInterceptor()
        .AddGrpcFluentValidation()
        .AddDataMappings()
        .AddBismuthCrypto()
        .AddDataMappings()
        .AddRepositories();

    builder.Services.AddGrpc();
}

var app = builder.Build();

app.MapGrpcService<UserManagerService>();

app.Run();
