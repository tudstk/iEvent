namespace IEvent.API.Models.UserModels
{
  public class UserModel
  {
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
  }
}
