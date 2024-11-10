public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        return await _context.Events.FindAsync(eventId);
    }

    public async Task AddEventAsync(Event evt)
    {
        await _context.Events.AddAsync(evt);
    }

    public async Task UpdateEventAsync(Event evt)
    {
        _context.Events.Update(evt);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}