using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.GenreServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.GenreServices
{
  public class GenreService : IGenreService
  {
    private readonly IRepository<Genre> genreRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public GenreService
      (IRepository<Genre> genreRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.genreRepository = genreRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }
    public async Task AddGenreAsync(AddGenreDto addGenreDto)
    {
      var newGenre = new Genre
      {
        Name = addGenreDto.Name,
        Description = addGenreDto.Description,
        IsDeleted = false,
      };

      await genreRepository.AddAsync(newGenre);
      await unitOfWork.CommitAsync();
    }

    public async Task DeleteByIdGenreAsync(int id)
    {
      var foundGenre = await genreRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundGenre != null)
      {
        foundGenre.IsDeleted = true;
        await unitOfWork.CommitAsync();
      }
    }

    public async Task<List<GetAllGenresDto>> GetAllGenresAsync()
    {
      return await genreRepository.Query()
        .Where(x => !x.IsDeleted)
        .Select(x => new GetAllGenresDto
        {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
        })
        .ToListAsync();
    }

    public async Task<GetByIdGenreDto> GetByIdGenreAsync(int id)
    {
      var foundGenre = await genreRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundGenre != null)
      {
        return new GetByIdGenreDto
        {
          Id = foundGenre.Id,
          Name = foundGenre.Name,
          Description = foundGenre.Description
        };
      }

      return null;
    }

    public async Task UpdateGenreAsync(UpdateGenreDto updateGenreDto)
    {
      var foundGenre = await genreRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == updateGenreDto.Id && !x.IsDeleted);

      if (foundGenre != null)
      {
        foundGenre.Name = updateGenreDto.Name;
        foundGenre.Description = updateGenreDto.Description;
        await unitOfWork.CommitAsync();
      }
    }
  }
}
