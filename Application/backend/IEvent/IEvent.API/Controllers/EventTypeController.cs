using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IEvent.Services.EventTypeServices;
using IEvent.Services.EventTypeServices.Dto;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/event-types")]
  public class EventTypeController : ControllerBase
  {
    private readonly IEventTypeService eventTypeService;

    public EventTypeController(IEventTypeService eventTypeService)
    {
      eventTypeService = eventTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var allEventTypes = await eventTypeService.GetAllEventTypesAsync();
        return Ok(allEventTypes);
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
        var evenType = await eventTypeService.GetByIdEventTypeAsync(id);
        if (evenType == null)
        {
          return NotFound();
        }
        return Ok(evenType);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(AddEventTypeDto addEventTypeDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await eventTypeService.AddEventTypeAsync(addEventTypeDto);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateEventTypeDto updateEventTypeDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await eventTypeService.UpdateEventTypeAsync(updateEventTypeDto);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await eventTypeService.DeleteByIdEventTypeAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}
