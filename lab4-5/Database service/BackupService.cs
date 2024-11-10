namespace DatabaseService
{
    public class BackupService
    {
        private readonly DatabasePool _databasePool;

        public BackupService(DatabasePool databasePool)
        {
            _databasePool = databasePool;
        }

        public void PerformBackup()
        {
            var connection = _databasePool.AcquireConnection();
            try
            {
                connection.ExecuteCommand("BACKUP DATABASE");
            }
            finally
            {
                _databasePool.ReleaseConnection(connection);
            }
        }
    }
}