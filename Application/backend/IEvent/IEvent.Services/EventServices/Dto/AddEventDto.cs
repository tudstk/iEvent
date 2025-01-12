namespace IEvent.Services.EventServices.Dto
{
  public class AddEventDto
  {
    public string Name { get; set; }

    public int CreatedByPersonId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime? Date { get; set; }

    public int? LocationId { get; set; }

    public int? EventTypeId { get; set; }

    public int? GenreId { get; set; }

    public int? MainArtistId { get; set; }

    public string? Theme { get; set; }
  }
}
