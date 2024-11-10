namespace DatabaseService
{
    public class DatabaseService
    {
        private readonly DatabasePool _databasePool;

        public DatabaseService(DatabasePool databasePool)
        {
            _databasePool = databasePool;
        }

        public void PerformDatabaseOperation(string command)
        {
            var connection = _databasePool.AcquireConnection();
            try
            {
                connection.ExecuteCommand(command);
            }
            finally
            {
                _databasePool.ReleaseConnection(connection);
            }
        }
    }
}