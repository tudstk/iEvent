using IEvent.Services.ArtistServices.Dto;

namespace IEvent.Services.ArtistServices
{
  public interface IArtistService
  {
    public Task AddArtistAsync(AddArtistDto addArtistDto);

    public Task UpdateArtistAsync(UpdateArtistDto updateArtistDto);

    public Task<List<GetAllArtistDto>> GetAllArtistAsync();

    public Task<GetByIdArtistDto> GetByIdArtistAsync(int id);

    public Task DeleteByIdArtistAsync(int id);
  }
}
