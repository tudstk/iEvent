namespace IEvent.Data.Entities
{
  public class UserEvent
  {
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EventId { get; set; }

    public bool IsPreffered { get; set; }

    public ApplicationUser User { get; set; }

    public Event Event { get; set; }
  }
}
