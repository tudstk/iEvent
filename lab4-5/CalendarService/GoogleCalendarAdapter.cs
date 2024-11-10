public class GoogleCalendarAdapter : ICalendarService
{
    private readonly GoogleCalendarApi _googleCalendarApi;

    public GoogleCalendarAdapter(GoogleCalendarApi googleCalendarApi)
    {
        _googleCalendarApi = googleCalendarApi;
    }

    public async Task AddEventAsync(Event evt)
    {
        await _googleCalendarApi.CreateEventAsync(evt.Title, evt.Date);
    }

    public async Task RemoveEventAsync(Event evt)
    {
        await _googleCalendarApi.DeleteEventAsync(evt.EventId.ToString());
    }

    public async Task SyncExternalEventsAsync()
    {
        await _googleCalendarApi.SyncEventsAsync();
    }
}