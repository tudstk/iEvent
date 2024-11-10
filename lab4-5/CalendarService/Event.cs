public class Event
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int TicketsAvailable { get; set; }
    public EventOrganizer Organizer { get; set; }
    public List<StandardUser> Attendees { get; set; }

    public void NotifyAttendees() { }
}