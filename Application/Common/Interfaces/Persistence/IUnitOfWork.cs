namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    public IMemberRepository MemberRepository { get; }
    Task CommitAsync();
}