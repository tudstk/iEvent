namespace IEvent.Services.UserServices.Dto
{
  public class ModifyProfileDto
  {
    public string UserName { get; set; }

    public string UserEmail { get; set; } = string.Empty;

    public List<int> ArtistsIds { get; set; }

    public List<int> LocationsIds { get; set; }

    public List<int> GenresIds { get; set; }

    public List<int> EventTypesIds { get; set; }
  }
}
