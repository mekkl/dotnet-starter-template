using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(nameof(Member));
        
        builder.HasKey(member => member.Id);
        builder.Property(member => member.Id)
            .HasDefaultValueSql("NEWID()")
            .IsRequired();
    }
}