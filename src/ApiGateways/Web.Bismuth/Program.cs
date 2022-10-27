using Web.Bismuth.Extensions;
using MediatR;
using Bismuth.Core;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .ConfigureOptions(builder.Configuration)
        .ConfigureLogging(builder.Configuration)
        .AddMediatR(typeof(Program).Assembly)
        .AddFluentValidation()
        .AddDataMappings()
        .AddGrpcServices()
        .AddSwagger();

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app
        .UseSwagger()
        .UseSwaggerUI()
        .UseExceptionHandler("/error")
        .UseHttpsRedirection()
        .UseAuthorization();

    app.MapControllers();
}

app.Run();
