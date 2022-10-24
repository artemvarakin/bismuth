using Web.Bismuth.Extensions;
using Web.Bismuth.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .ConfigureOptions(builder.Configuration)
        .ConfigureLogging(builder.Configuration)
        .AddMediatR(typeof(Program).Assembly)
        .AddValidation()
        .AddDataMappings()
        .AddGrpcServices()
        .AddSwagger();

    builder.Services.AddControllers(options =>
        options.Filters.Add(new ModelStateFilter()));
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
