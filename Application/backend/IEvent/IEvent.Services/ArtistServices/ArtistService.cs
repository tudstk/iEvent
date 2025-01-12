using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.ArtistServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.ArtistServices
{
  public class ArtistService : IArtistService
  {
    private readonly IRepository<Artist> artistRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public ArtistService
      (IRepository<Artist> artistRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.artistRepository = artistRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }
    public async Task AddArtistAsync(AddArtistDto addArtistDto)
    {
      var newArtist = new Artist
      {
        Name = addArtistDto.Name,
        Description = addArtistDto.Description,
        IsDeleted = false,
      };

      await artistRepository.AddAsync(newArtist);
      await unitOfWork.CommitAsync();
    }

    public async Task DeleteByIdArtistAsync(int id)
    {
      var foundArtist = await artistRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundArtist != null)
      {
        foundArtist.IsDeleted = true;
        await unitOfWork.CommitAsync();
      }
    }

    public async Task<List<GetAllArtistDto>> GetAllArtistAsync()
    {
      return await artistRepository.Query()
        .Where(x => !x.IsDeleted)
        .Select(x => new GetAllArtistDto
        {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
        })
        .ToListAsync();
    }

    public async Task<GetByIdArtistDto> GetByIdArtistAsync(int id)
    {
      var foundArtist = await artistRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundArtist != null)
      {
        return new GetByIdArtistDto
        {
          Id = foundArtist.Id,
          Name = foundArtist.Name,
          Description = foundArtist.Description
        };
      }

      return null;
    }

    public async Task UpdateArtistAsync(UpdateArtistDto updateArtistDto)
    {
      var foundArtist = await artistRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == updateArtistDto.Id && !x.IsDeleted);

      if (foundArtist != null)
      {
        foundArtist.Name = updateArtistDto.Name;
        foundArtist.Description = updateArtistDto.Description;
        await unitOfWork.CommitAsync();
      }
    }
  }
}
