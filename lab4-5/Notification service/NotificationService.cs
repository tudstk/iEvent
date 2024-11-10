namespace NotificationService
{
    public class NotificationService : ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Notification notification)
        {
            foreach (var observer in _observers)
            {
                observer.Update(notification);
            }
        }

        public void SendNotification(string type, string message, string destination)
        {
            var notification = new Notification(type, message, destination);
            Notify(notification);
        }
    }
}