namespace IEvent.Data.Entities
{
  public class Event
  {
    public int Id { get; set; }

    public int CreatedByPersonId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public int? LocationId { get; set; }

    public int? EventTypeId { get; set; }

    public int? GenreId { get; set; }

    public int? MainArtistId { get; set; }

    public string? Theme { get; set; }

    public bool IsDeleted { get; set; }

    public Location Location { get; set; }

    public EventType EventType { get; set; }

    public Genre Genre { get; set; }

    public Artist MainArtist { get; set; }

    public List<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
  }
}
