namespace IEvent.Services.EventServices.Dto
{
  public class GetByIdEventDto
  {
    public int Id { get; set; }

    public int CreatedByPersonId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public string? LocationName { get; set; }

    public string? EventTypeName { get; set; }

    public string? GenreName { get; set; }

    public string? MainArtistName { get; set; }

    public string? Theme { get; set; }
  }
}
