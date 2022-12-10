using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class AppDbContextInitializer
{
    private readonly ILogger<AppDbContextInitializer> _logger;
    private readonly AppDbContext _context;

    public AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
            else
            {
                _logger.LogWarning("Not performing migrations!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // setup seed logic
        
        // Initialize roles
        // foreach (var role in Role.GetValues())
        // {
        //     if (await _context.Roles.FindAsync(role.Id) is null)
        //         _context.Roles.Add(role);
        // }
        // await _context.SaveChangesAsync();
        //
        // // Initialize permissions
        // var permissions = Enum.GetValues<Domain.Enums.Permission>()
        //     .Select(permission => new Permission { Id = (int)permission, Name = permission.ToString() });
        //
        // foreach (var permission in permissions)
        // {
        //     if (await _context.Permissions.FindAsync(permission.Id) is null)
        //         _context.Permissions.Add(permission);
        // }
        // await _context.SaveChangesAsync();
        //
        // // Initialize role permission relations
        // var adminRole = await _context.Roles.Include(role => role.Permissions)
        //     .SingleOrDefaultAsync(role => role.Id == Role.Admin.Id);
        //
        // var persistedPermissions = await _context.Permissions.Where(permission =>
        //     permission.Id >= 300 && permission.Id <= 499)
        //     .ToListAsync();
        //
        // persistedPermissions.Where(permission => !adminRole!.Permissions.Select(p => p.Id).Contains(permission.Id))
        //     .ToList()
        //     .ForEach(permission => adminRole!.Permissions.Add(permission));
        // await _context.SaveChangesAsync();
        //
        // // Initialize admin
        // if (!_context.Members.Any())
        //     _context.Members.Add(new Member() { Name = "Mekkl", Roles = new List<Role>( new[] { adminRole! } )});
        //
        // await _context.SaveChangesAsync();
    }
}