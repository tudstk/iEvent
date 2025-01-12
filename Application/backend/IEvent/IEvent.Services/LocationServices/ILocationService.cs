using IEvent.Services.LocationServices.Dto;

namespace IEvent.Services.LocationServices
{
  public interface ILocationService
  {
    public Task AddLocationAsync(AddLocationDto addLocationDto);

    public Task UpdateLocationAsync(UpdateLocationDto updateLocationDto);

    public Task<List<GetAllLocationsDto>> GetAllLocationsAsync();

    public Task<GetByIdLocationDto> GetByIdLocationAsync(int id);

    public Task DeleteByIdLocationAsync(int id);
  }
}
