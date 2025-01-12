namespace IEvent.Data.Entities
{
  public class Artist
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public List<UserFavoriteArtist> UserFavoriteArtists { get; set; } = new List<UserFavoriteArtist>();

    public List<Event> ArtistEvents { get; set; } = new List<Event>();
  }
}
