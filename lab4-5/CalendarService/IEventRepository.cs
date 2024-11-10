public interface IEventRepository
{
    Task<Event> GetEventByIdAsync(int eventId);
    Task AddEventAsync(Event evt);
    Task UpdateEventAsync(Event evt);
    Task SaveChangesAsync();
}