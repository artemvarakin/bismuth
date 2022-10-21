using MediatR;
using Web.Bismuth.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .ConfigureOptions(builder.Configuration)
        .ConfigureLogging(builder.Configuration)
        .AddMediatR(typeof(Program).Assembly)
        .AddDataMappings()
        .AddGrpcServices()
        .AddControllers();


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();
