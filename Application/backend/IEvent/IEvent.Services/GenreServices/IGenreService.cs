using IEvent.Services.GenreServices.Dto;

namespace IEvent.Services.GenreServices
{
  public interface IGenreService
  {
    public Task AddGenreAsync(AddGenreDto addGenreDto);

    public Task UpdateGenreAsync(UpdateGenreDto updateGenreDto);

    public Task<List<GetAllGenresDto>> GetAllGenresAsync();

    public Task<GetByIdGenreDto> GetByIdGenreAsync(int id);

    public Task DeleteByIdGenreAsync(int id);
  }
}
