using Application;
using Application.Admin.Queries;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using MinimalApi.Extensions;
using MinimalApi.Health;
using MinimalApi.HostedServices;
using MinimalApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

builder.Services.AddSwagger();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();
builder.Services.AddAppHealthChecks();

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

app.MapGet("/ping", () => "pong")
    .WithName("Ping")
    .WithDisplayName("Ping")
    .WithSummary("Ping pong endpoint")
    .WithDescription("Get a pong response from server")
    .WithOpenApi();

app.MapGet("/admin/time", (IMediator mediator) => mediator.Send(new GetServerTimeQuery()))
    .WithName("Server Time")
    .WithDisplayName("Server Time")
    .WithSummary("Display server time")
    .WithDescription("Get the current server time")
    .WithOpenApi();

app.Run();