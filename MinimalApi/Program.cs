using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
