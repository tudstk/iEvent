namespace NotificationService
{
    public class UserObserver : IObserver
    {
        public string UserName { get; }

        public UserObserver(string userName)
        {
            UserName = userName;
        }

        public void Update(Notification notification)
        {
            Console.WriteLine($"User {UserName} received notification: {notification}");
        }
    }
}