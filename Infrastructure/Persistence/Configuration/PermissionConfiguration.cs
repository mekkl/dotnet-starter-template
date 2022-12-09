using Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        
        builder.HasKey(permission => permission.Id);

        var permissions = Enum.GetValues<Domain.Auth.Enums.Permission>()
            .Select(permission => new Permission { Id = (int)permission, Name = permission.ToString() });

        builder.HasData(permissions);
    }
}