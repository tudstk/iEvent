using IEvent.Services.ArtistServices;
using IEvent.Services.ArtistServices.Dto;
using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/artists")]
  public class ArtistsController : ControllerBase
  {
    private readonly IArtistService artistService;

    public ArtistsController(IArtistService artistService)
    {
      artistService = artistService;
    }

    
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
      try
      {
        var allArtists = await artistService.GetAllArtistAsync();
        return Ok(allArtists);
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
        var artist = await artistService.GetByIdArtistAsync(id);
        if (artist == null)
        {
          return NotFound();
        }
        return Ok(artist);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

  [Authorize(Roles = AuthRoles.Admin)]
  [HttpPost]
  public async Task<IActionResult> Create(AddArtistDto addArtistDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

      try
      {
        await artistService.AddArtistAsync(addArtistDto);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
  }

  [Authorize(Roles = AuthRoles.Admin)]
  [HttpPut("{id}")]
  public async Task<IActionResult> Update(UpdateArtistDto updateArtistDto)
  {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await artistService.UpdateArtistAsync(updateArtistDto);
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
        await artistService.DeleteByIdArtistAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
}
}
