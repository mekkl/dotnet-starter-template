using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    public IMemberRepository MemberRepository { get; }
    
    public UnitOfWork(DbContext context, IMemberRepository memberRepository)
    {
        _context = context;
        MemberRepository = memberRepository;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _context.Dispose();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}