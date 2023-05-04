using System.Data.Common;
using Application.Common.Interfaces.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MinimalApi;
using Testcontainers.MsSql;

namespace Tests.IntegrationTests;

public class WebAppFactory : WebApplicationFactory<IWebAppEntry>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithPassword("yourStrong(!)Password")
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => { logging.ClearProviders(); });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new IntegrationTestDbConnectionFactory(
                    $"{_dbContainer.GetConnectionString()};TrustServerCertificate=True"));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    private class IntegrationTestDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public IntegrationTestDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}