using System.ComponentModel.DataAnnotations;

namespace IEvent.API.Models.Auth
{
  public class RegisterModel
  {
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
    public string Password { get; set; } = string.Empty;
  }
}
