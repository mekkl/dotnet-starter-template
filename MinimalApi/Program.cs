using Application;
using Application.Admin.Queries;
using Application.Auth.Commands;
using Domain.Auth.Enums;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Auth;
using MinimalApi.Extensions;
using MinimalApi.Health;
using MinimalApi.HostedServices;
using MinimalApi.Middleware;
using MinimalApi.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

builder.Services.AddSwagger();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();
builder.Services.AddAppHealthChecks();

builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Permission.ReadMember.ToString(),
        policy => policy.RequireClaim("Permission", Permission.ReadMember.ToString()));
    options.AddPolicy(Permission.AccessMembers.ToString(),
        policy => policy.RequireClaim("Permission", Permission.AccessMembers.ToString()));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddHostedService<TimedHostedService>();

var app = builder.Build();

app.UseErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();

    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();
}
else
{
    // Initialise database
    using var scope = app.Services.CreateScope();

    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitialiseAsync();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwaggerSetup();

app.UsePerformanceMiddleware();
app.UseHealthChecks();
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/ping", () => "pong")
    .WithName("Ping")
    .WithDisplayName("Ping")
    .WithSummary("Ping pong endpoint")
    .WithDescription("Get a pong response from server")
    .WithOpenApi();

app.MapGet("/admin/servertime", (IMediator mediator) => mediator.Send(new GetServerTimeQuery()))
    .WithName("Server Time")
    .WithDisplayName("Server Time")
    .WithSummary("Display server time")
    .WithDescription("Get the current server time")
    .WithOpenApi();

app.MapGet("/auth/debug",  [HasPermission(Permission.ReadMember)] () => "OK")
    .WithName("Auth Debug")
    .WithDisplayName("Auth Debug")
    .WithSummary("Auth Debug endpoint")
    .WithDescription("Test auth related code")
    .WithOpenApi();

app.MapPost("/auth/login", (IMediator mediator, [FromBody] LoginCommand loginCommand) 
        => mediator.Send(loginCommand))
    .WithName("Login")
    .WithDisplayName("Login")
    .WithSummary("Login user")
    .WithDescription("Authenticates and login the member")
    .WithOpenApi();

app.Run();