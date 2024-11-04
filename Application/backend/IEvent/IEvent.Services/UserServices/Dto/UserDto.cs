namespace IEvent.Services.UserServices.Dto
{
  public class UserDto
  {
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
  }
}
