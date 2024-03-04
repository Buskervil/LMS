using System.Data;

namespace Lms.Core.Application
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        IDbTransaction GetOpenTransaction();
        IDbConnection GetOpenWriteConnection();
        IDbTransaction GetOpenWriteTransaction();
    }
}