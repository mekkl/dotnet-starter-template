using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Auth;
using MinimalApi.Endpoints;
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

builder.Services.AddEndpoints();

builder.Services.AddSwagger();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();
builder.Services.AddAppHealthChecks();

builder.Services.AddCors();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

builder.Services.AddAuthorization();
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

app.MapEndpoints();


app.Run();