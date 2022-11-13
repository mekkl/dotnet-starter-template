namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    Task CommitAsync();
}