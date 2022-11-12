using System.Data.Common;
using Application.Common.Interfaces.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class AppDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
        
    public AppDbConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;
    }
    public DbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        return connection;
    }
}