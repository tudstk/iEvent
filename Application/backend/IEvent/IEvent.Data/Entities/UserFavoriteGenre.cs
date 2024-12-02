namespace IEvent.Data.Entities
{
  public class UserFavoriteGenre
  {
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GenreId { get; set; }

    public ApplicationUser User { get; set; }

    public Genre Genre { get; set; }
  }
}
