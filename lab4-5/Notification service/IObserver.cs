namespace NotificationService
{
    public interface IObserver
    {
        void Update(Notification notification);
    }
}