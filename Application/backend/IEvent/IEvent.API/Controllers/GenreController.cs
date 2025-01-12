using IEvent.Shared.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IEvent.Services.GenreServices;
using IEvent.Services.GenreServices.Dto;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("api/genres")]
  public class GenreController : ControllerBase
  {
    private readonly IGenreService genreService;

    public GenreController(IGenreService genreService)
    {
      this.genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var allGenres = await genreService.GetAllGenresAsync();
        return Ok(allGenres);
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
        var genre = await genreService.GetByIdGenreAsync(id);
        if (genre == null)
        {
          return NotFound();
        }
        return Ok(genre);
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(AddGenreDto addGenreDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await genreService.AddGenreAsync(addGenreDto);

        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateGenreDto updateGenreDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await genreService.UpdateGenreAsync(updateGenreDto);
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
        await genreService.DeleteByIdGenreAsync(id);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500);
      }
    }
  }
}
