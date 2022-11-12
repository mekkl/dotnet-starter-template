using System.Reflection;
using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IDbConnectionFactory dbConnectionFactory)
        : base(options)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = _dbConnectionFactory.CreateConnectionAsync().Result;
        optionsBuilder.UseSqlServer(connection);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}