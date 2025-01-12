namespace IEvent.Data.Entities
{
  public class EventType
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
  }
}
