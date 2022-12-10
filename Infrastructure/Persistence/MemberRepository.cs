using Application.Common.Interfaces.Persistence;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class MemberRepository : Repository<Member>, IMemberRepository
{
    public MemberRepository(DbContext context) : base(context)
    {
    }
}