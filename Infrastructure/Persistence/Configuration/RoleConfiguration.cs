using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));
        
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id).ValueGeneratedNever();
        
        builder.Property(role => role.Name);

        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(role => role.Members)
            .WithMany(member => member.Roles);
    }
}