using System.Data.Common;

namespace Application.Common.Interfaces.Persistence;

public interface IDbConnectionFactory
{
    public DbConnection CreateConnection();
}