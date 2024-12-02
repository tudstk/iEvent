namespace IEvent.Data.Entities
{
  public class Location
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public List<UserFavoriteLocation> UserFavoriteLocations { get; set; } = new List<UserFavoriteLocation>();

    public List<Event> LocationEvents { get; set; } = new List<Event>();
  }
}
