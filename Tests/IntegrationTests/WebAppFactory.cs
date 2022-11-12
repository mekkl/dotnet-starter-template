using System.Data.Common;
using Application.Common.Interfaces.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MinimalApi;

namespace Tests.IntegrationTests;

public class WebAppFactory : WebApplicationFactory<IWebAppEntry>, IAsyncLifetime
{
    private readonly MsSqlTestcontainer _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration()
        {
            Database = "testDb",
            Password = "yourStrong(!)Password"
        })
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
        
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new IntegrationTestDbConnectionFactory($"{_dbContainer.ConnectionString};TrustServerCertificate=True"));
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
    
        public async Task<DbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}