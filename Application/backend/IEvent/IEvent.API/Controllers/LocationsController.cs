using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IEvent.Services.LocationServices;
using IEvent.Services.LocationServices.Dto;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/locations")]
  public class LocationsController : ControllerBase
  {
    private readonly ILocationService locationService;

    public LocationsController(ILocationService locationService)
    {
      locationService = locationService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var allLocations = await locationService.GetAllLocationsAsync();
        return Ok(allLocations);
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
        var location = await locationService.GetByIdLocationAsync(id);
        if (location == null)
        {
          return NotFound();
        }
        return Ok(location);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(AddLocationDto addLocationDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await locationService.AddLocationAsync(addLocationDto);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateLocationDto updateLocationDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await locationService.UpdateLocationAsync(updateLocationDto);
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
        await locationService.DeleteByIdLocationAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}
