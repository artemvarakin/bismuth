global using BismuthAPI.Data;
global using Microsoft.EntityFrameworkCore;
using BismuthAPI.Abstractions;
using BismuthAPI.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("BismuthDb")));

    builder.Services.AddCors(options => {
        options.AddDefaultPolicy(builder => {
            builder
                .WithOrigins("http://localhost:4200/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    builder.Services
        .AddTransient<IProjectRepository, ProjectRepository>()
        .AddTransient<IIssueRepository, IssueRepository>();

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

    //app.UseHttpsRedirection();

    app.UseCors(builder => {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}