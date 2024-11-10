namespace DatabaseService
{
    public class DatabasePool
    {
        private readonly List<PooledDatabaseConnection> _availableConnections;
        private readonly List<PooledDatabaseConnection> _usedConnections;
        private readonly int _maxPoolSize;

        public DatabasePool(int maxPoolSize)
        {
            _maxPoolSize = maxPoolSize;
            _availableConnections = new List<PooledDatabaseConnection>();
            _usedConnections = new List<PooledDatabaseConnection>();

            for (int i = 0; i < maxPoolSize / 2; i++)
            {
                _availableConnections.Add(new PooledDatabaseConnection($"Conn-{i + 1}"));
            }
        }

        public PooledDatabaseConnection AcquireConnection()
        {
            lock (_availableConnections)
            {
                if (_availableConnections.Count > 0)
                {
                    var connection = _availableConnections[0];
                    _usedConnections.Add(connection);
                    _availableConnections.RemoveAt(0);
                    connection.Connect();
                    return connection;
                }
                else if (_usedConnections.Count < _maxPoolSize)
                {
                    var newConnection = new PooledDatabaseConnection($"Conn-{_usedConnections.Count + 1}");
                    _usedConnections.Add(newConnection);
                    newConnection.Connect();
                    return newConnection;
                }
                else
                {
                    throw new InvalidOperationException("No available connections.");
                }
            }
        }

        public void ReleaseConnection(PooledDatabaseConnection connection)
        {
            lock (_availableConnections)
            {
                if (_usedConnections.Remove(connection))
                {
                    connection.Disconnect();
                    _availableConnections.Add(connection);
                }
            }
        }
    }
}