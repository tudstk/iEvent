namespace IEvent.Data.Entities
{
  public class UserFavoriteLocation
  {
    public int Id { get; set; }

    public int UserId { get; set; }

    public int LocationId { get; set; }

    public ApplicationUser User { get; set; }

    public Location Location { get; set; }
  }
}
