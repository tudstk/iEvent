using IEvent.Services.Shared;

namespace IEvent.Services.UserServices.Dto
{
  public class GetProfileDto
  {
    public int PersonId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public List<DropdownOption> MyArtists { get; set; }

    public List<DropdownOption> MyLocations { get; set; }

    public List<DropdownOption> MyGenres { get; set; }

    public List<DropdownOption> MyEventTypes { get; set; }
  }
}
