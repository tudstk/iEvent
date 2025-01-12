using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IEvent.Services.EventServices;
using IEvent.Services.EventServices.Dto;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/events")]
  public class EventController : ControllerBase
  {
    private readonly IEventService eventService;

    public EventController(IEventService eventService)
    {
      this.eventService = eventService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var allEvents = await eventService.GetAllEventsAsync();
        return Ok(allEvents);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
        var foundEvent = await eventService.GetByIdEventAsync(id);
        if (foundEvent == null)
        {
          return NotFound();
        }
        return Ok(foundEvent);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Organizer}")]
    [HttpPost]
    public async Task<IActionResult> Create(AddEventDto addEventDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await eventService.AddEventAsync(addEventDto);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateEventDto updateEventDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        if (HttpContext.User.IsInRole(AuthRoles.Admin))
        {
          await eventService.UpdateEventAsync(updateEventDto);
          return Ok();
        }

        if (HttpContext.User.IsInRole(AuthRoles.Organizer))
        {
          var eventDetails = await eventService.GetByIdEventAsync(updateEventDto.Id);
          if (eventDetails == null)
          {
            return NotFound();
          }

          var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (eventDetails.CreatedByPersonId.ToString() != userId)
          {
            return Forbid("You are not authorized to delete this event.");
          }

          await eventService.UpdateEventAsync(updateEventDto);
          return Ok();
        }

        return Forbid("You do not have the required permissions.");
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        if (HttpContext.User.IsInRole(AuthRoles.Admin))
        {
          await eventService.DeleteByIdEventAsync(id);
          return Ok();
        }

        if (HttpContext.User.IsInRole(AuthRoles.Organizer))
        {
          var eventDetails = await eventService.GetByIdEventAsync(id);
          if (eventDetails == null)
          {
            return NotFound();
          }

          var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
          if (eventDetails.CreatedByPersonId.ToString() != userId)
          {
            return Forbid("You are not authorized to delete this event.");
          }

          await eventService.DeleteByIdEventAsync(id);
          return Ok();
        }

        return Forbid("You do not have the required permissions.");
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}
