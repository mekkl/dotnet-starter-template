namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}