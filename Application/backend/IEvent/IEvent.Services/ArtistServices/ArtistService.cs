using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.ArtistServices.Dto;
using IEvent.Shared.Settings;
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
    public Task AddArtistAsync(AddArtistDto addArtistDto)
    {
      throw new NotImplementedException();
    }

    public Task DeleteByIdArtistAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<List<GetAllArtistDto>> GetAllArtistAsync()
    {
      throw new NotImplementedException();
    }

    public Task<GetByIdArtistDto> GetByIdArtistAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task UpdateArtistAsync(UpdateArtistDto updateArtistDto)
    {
      throw new NotImplementedException();
    }
  }
}
