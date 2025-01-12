namespace IEvent.Data.Entities
{
  public class Genre
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public List<UserFavoriteGenre> UserFavoriteGenres { get; set; } = new List<UserFavoriteGenre>();

    public List<Event> GenreEvents { get; set; } = new List<Event>();
  }
}
