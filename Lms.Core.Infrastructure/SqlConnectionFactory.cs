using System.Data;
using Lms.Core.Application;
using Lms.Core.Application.Connections;
using Npgsql;

namespace Lms.Core.Infrastructure
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        private IDbConnection? _readOnlyConnection;
        private IDbConnection? _writeConnection;
        private IDbTransaction? _writeTransaction;
        private IDbTransaction? _readOnlyTransaction;

        public SqlConnectionFactory(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        /// <summary>
        /// Использовать только для операций записи, для остального есть GetOpenReadonlyConnection
        /// </summary>
        public IDbConnection GetOpenWriteConnection()
        {
            CreateWriteConnectionAndTransaction();

            return _writeConnection!;
        }

        public IDbTransaction GetOpenWriteTransaction()
        {
            CreateWriteConnectionAndTransaction();

            return _writeTransaction!;
        }

        public IDbConnection GetOpenConnection()
        {
            CreateConnectionAndTransaction();

            return _readOnlyConnection!;
        }

        public IDbTransaction GetOpenTransaction()
        {
            CreateConnectionAndTransaction();

            return _readOnlyTransaction!;
        }

        private void CreateWriteConnectionAndTransaction()
        {
            if (_writeConnection != null)
            {
                return;
            }

            _writeConnection = new NpgsqlConnection(_connectionStringProvider.DefaultConnectionString);
            _writeConnection.Open();
            _writeTransaction = _writeConnection.BeginTransaction(); // implicit read committed
        }

        private void CreateConnectionAndTransaction()
        {
            if (_readOnlyConnection != null)
            {
                return;
            }

            _readOnlyConnection = new NpgsqlConnection(_connectionStringProvider.DefaultConnectionString);
            _readOnlyConnection.Open();
            _readOnlyTransaction = _readOnlyConnection.BeginTransaction(); // implicit read committed
        }

        public void Dispose()
        {
            if (_readOnlyTransaction != null)
            {
                try
                {
                    _readOnlyTransaction.Rollback();
                }
                catch
                {
                    // ignored
                }

                _readOnlyTransaction.Dispose();
            }

            _readOnlyConnection?.Dispose();
            _writeTransaction?.Dispose();
            _writeConnection?.Dispose();
        }
    }
}