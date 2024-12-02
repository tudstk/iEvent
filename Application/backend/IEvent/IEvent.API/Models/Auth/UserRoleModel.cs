using System.ComponentModel.DataAnnotations;

namespace IEvent.API.Models.Auth
{
  public class UserRoleModel
  {
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; } = string.Empty;
  }
}
