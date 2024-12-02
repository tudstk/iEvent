namespace IEvent.Data.Entities
{
  public class UserFavoriteArtist
  {
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ArtistId { get; set; }

    public ApplicationUser User { get; set; }

    public Artist Artist { get; set; }
  }
}
