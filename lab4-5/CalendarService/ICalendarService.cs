public interface ICalendarService
{
    Task AddEventAsync(Event evt);
    Task RemoveEventAsync(Event evt);
    Task SyncExternalEventsAsync();
}