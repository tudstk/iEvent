using IEvent.API.Models.Auth;
using IEvent.Data.Entities;
using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IEvent.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(
      UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole<int>> roleManager,
      IConfiguration configuration)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (!await _roleManager.RoleExistsAsync(AuthRoles.User))
      {
        return BadRequest("The role user does not exist");
      }

      var existingUserByName = await _userManager.FindByNameAsync(model.Username);
      if (existingUserByName != null)
      {
        return BadRequest(new { message = "A user with this username already exists" });
      }

      var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
      if (existingUserByEmail != null)
      {
        return BadRequest(new { message = "A user with this email already exists" });
      }

      var user = new ApplicationUser
      {
        UserName = model.Username,
        Email = model.Email
      };

      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(user, AuthRoles.User);
        return Ok(new { message = "User registered successfully" });
      }

      return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.FindByNameAsync(model.Username);
      if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
      {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim("userId", user.Id.ToString()),
          new Claim("email", user.Email!),
        };

        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Issuer"],
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["ExpiryMinues"]!)),
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]!)),
            SecurityAlgorithms.HmacSha256));

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
      }

      return Unauthorized();
    }

    //[Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("add-role")]
    public async Task<IActionResult> AddRole([FromBody] string role)
    {
      if (string.IsNullOrEmpty(role))
      {
        return BadRequest("Role must be provided");
      }

      if (!await _roleManager.RoleExistsAsync(role))
      {
        var result = await _roleManager.CreateAsync(new IdentityRole<int>(role));
        if (result.Succeeded)
        {
          return Ok(new { message = "Role added successfully" });
        }

        return BadRequest(result.Errors);
      }

      return BadRequest("Role already exists");
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] UserRoleModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.FindByNameAsync(model.Username);
      if (user == null)
      {
        return BadRequest("User not found");
      }

      if (!await _roleManager.RoleExistsAsync(model.Role))
      {
        return BadRequest("Role not found");
      }

      var result = await _userManager.AddToRoleAsync(user, model.Role);
      if (result.Succeeded)
      {
        return Ok(new { message = "Role assigned successfully" });
      }

      return BadRequest(result.Errors);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("remove-role")]
    public async Task<IActionResult> RemoveRole([FromBody] UserRoleModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.FindByNameAsync(model.Username);
      if (user == null)
      {
        return BadRequest("User not found");
      }

      if (!await _roleManager.RoleExistsAsync(model.Role))
      {
        return BadRequest("Role not found");
      }

      var result = await _userManager.RemoveFromRoleAsync(user, model.Role);
      if (result.Succeeded)
      {
        return Ok(new { message = "Role removed successfully from the user" });
      }

      return BadRequest(result.Errors);
    }
  }
}