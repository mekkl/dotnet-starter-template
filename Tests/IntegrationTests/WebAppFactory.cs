using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using MinimalApi;

namespace Tests.IntegrationTests;

public class WebAppFactory : WebApplicationFactory<IWebAppEntry>, IAsyncLifetime
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
        
        builder.ConfigureServices(services =>
        {
            // ... Configure test services
        });
    }
    
    public Task InitializeAsync()
    {
        // ... Configure before
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        // ... Configure after
        return Task.CompletedTask;
    }
}