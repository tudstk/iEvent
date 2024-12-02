using System.ComponentModel.DataAnnotations;

namespace IEvent.API.Models.Auth
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
    public string Password { get; set; } = string.Empty;
  }
}
