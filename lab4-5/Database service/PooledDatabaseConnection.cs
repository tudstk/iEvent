namespace DatabaseService
{
    public class PooledDatabaseConnection
    {
        public string ConnectionId { get; }

        public PooledDatabaseConnection(string connectionId)
        {
            ConnectionId = connectionId;
        }

        public void Connect()
        {
            Console.WriteLine($"Connection {ConnectionId} established.");
        }

        public void Disconnect()
        {
            Console.WriteLine($"Connection {ConnectionId} closed.");
        }

        public void ExecuteCommand(string command)
        {
            Console.WriteLine($"Executing '{command}' on connection {ConnectionId}.");
        }
    }
}