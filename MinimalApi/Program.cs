using Application;
using Infrastructure;
using MinimalApi.Health;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddAppHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");
app.UseHealthChecks();

app.Run();