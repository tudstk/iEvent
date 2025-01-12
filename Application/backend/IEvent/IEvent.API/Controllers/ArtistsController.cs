using IEvent.API.Models.UserModels;
using IEvent.Services.UserServices;
using IEvent.Services.UserServices.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/artists")]
  public class ArtistsController : ControllerBase
  {
    private readonly IUserService _userService;

    public ArtistsController(IUserService userService)
    {
      _userService = userService;
    }

    {}
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var users = await _userService.GetAllUsersAsync();
      return Ok(users);
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<IActionResult> GetById(int id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      return Ok(user);
    }

    // POST: api/Users
    [HttpPost]
    public async Task<IActionResult> Create(UserModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userDto = new UserDto
      {
        Name = model.Name,
        Description = model.Description
      };

      var createdUserId = await _userService.AddUserAsync(userDto);

      return CreatedAtRoute("GetUserById", new { id = createdUserId }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userDto = new UserDto
      {
        Name = model.Name,
        Description = model.Description
      };

      var updated = await _userService.UpdateUserAsync(id, userDto);

      if (!updated)
      {
        return NotFound();
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var deleted = await _userService.DeleteUserAsync(id);

      if (!deleted)
      {
        return NotFound();
      }

      return NoContent();
    }
  }
}
