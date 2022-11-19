using Microsoft.OpenApi.Models;

namespace MinimalApi.Extensions;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = ".NET 7 swagger",
                Description = ".NET 7 starter template swagger doc",
                TermsOfService = new Uri("https://github.com/mekkl/dotnet-starter-template"),
                Contact = new OpenApiContact
                {
                    Name = "Mekkl",
                    Url = new Uri("https://github.com/mekkl")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/mekkl/dotnet-starter-template/blob/main/LICENSE")
                }
            });
        });
    }

    public static void UseSwaggerSetup(this WebApplication? app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }
}