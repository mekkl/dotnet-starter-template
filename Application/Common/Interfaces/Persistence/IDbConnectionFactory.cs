using System.Data.Common;

namespace Application.Common.Interfaces.Persistence;

public interface IDbConnectionFactory
{
    public Task<DbConnection> CreateConnectionAsync();
}