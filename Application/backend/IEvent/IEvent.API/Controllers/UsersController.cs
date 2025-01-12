using IEvent.Services.UserServices;
using IEvent.Services.UserServices.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/users")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
      this.userService = userService;
    }

    [Authorize]
    [HttpPost("/event")]
    public async Task<IActionResult> AddEventForUser([FromBody] int eventId)
    {
      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        await userService.AddEventForUser(userId, eventId);

        return Ok();
        
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpGet("/api/users/profile")]
    public async Task<IActionResult> GetProfileAsync()
    {
      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        var profile = await userService.GetProfileAsync(userId);

        if (profile == null)
        {
          return NotFound();
        }

        return Ok(profile);

      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpGet("/recommendations")]
    public async Task<IActionResult> GetRecommendedUserEvents()
    {
      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        var recommendations = await userService.GetRecommendedUserEvents(userId);

        return Ok(recommendations);

      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpGet("/all-user-events")]
    public async Task<IActionResult> GetUserEvents()
    {
      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        var events = await userService.GetUserEvents(userId);

        return Ok(events);

      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpDelete("/event")]
    public async Task<IActionResult> RemoveEventForUser([FromBody] int eventId)
    {
      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        await userService.RemoveEventForUser(userId, eventId);

        return Ok();

      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpPut("/api/users/profile")]
    public async Task<IActionResult> UpdateProfileAsync([FromBody] ModifyProfileDto modifyProfileDto)
    {

      if (modifyProfileDto == null)
      {
        return BadRequest();
      }

      try
      {
        var userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

        await userService.UpdateProfileAsync(userId, modifyProfileDto);

        return Ok();

      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}
