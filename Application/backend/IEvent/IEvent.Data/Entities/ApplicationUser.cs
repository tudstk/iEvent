using Microsoft.AspNetCore.Identity;

namespace IEvent.Data.Entities
{
  public class ApplicationUser : IdentityUser<int>
  {
    public List<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

    public List<UserFavoriteLocation> UserFavoriteLocations { get; set; } = new List<UserFavoriteLocation>();

    public List<UserFavoriteGenre> UserFavoriteGenres { get; set; } = new List<UserFavoriteGenre>();

    public List<UserFavoriteArtist> UserFavoriteArtists { get; set; } = new List<UserFavoriteArtist>();
  }
}
