using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Id)
            .HasDefaultValueSql("NEWID()")
            .IsRequired();
        
        builder.Property(user => user.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}