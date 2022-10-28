using Bismuth.Core;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using AuthAPI.Extensions;
using AuthAPI.Grpc;

var builder = WebApplication.CreateBuilder(args);
{
    // Setup a HTTP/2 endpoint without TLS. Only for development.
    builder.WebHost.ConfigureKestrel(options =>
        options.ListenLocalhost(5129, o => o.Protocols = HttpProtocols.Http2));

    builder.Services
        .AddOptionsConfiguration(builder.Configuration)
        .AddMediatR(typeof(Program).Assembly)
        .AddDataMappings()
        .AddServices()
        .AddRepositories();

    builder.Services.AddGrpc();
}

var app = builder.Build();

app.MapGrpcService<SessionService>();

app.Run();
