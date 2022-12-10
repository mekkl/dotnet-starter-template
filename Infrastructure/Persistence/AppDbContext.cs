using System.Reflection;
using Application.Common.Interfaces.Persistence;
using Domain.Model;
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
    
    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = _dbConnectionFactory.CreateConnection();
        optionsBuilder.UseSqlServer(connection, builder =>
        {
            builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}