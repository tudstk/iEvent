using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.LocationServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.LocationServices
{
  public class LocationService : ILocationService
  {
    private readonly IRepository<Location> locationRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public LocationService
      (IRepository<Location> locationRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.locationRepository = locationRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }
    public async Task AddLocationAsync(AddLocationDto addLocationDto)
    {
      var newLocation = new Location
      {
        Name = addLocationDto.Name,
        Description = addLocationDto.Description,
        IsDeleted = false,
      };

      await locationRepository.AddAsync(newLocation);
      await unitOfWork.CommitAsync();
    }

    public async Task DeleteByIdLocationAsync(int id)
    {
      var foundLocation = await locationRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundLocation != null)
      {
        foundLocation.IsDeleted = true;
        await unitOfWork.CommitAsync();
      }
    }

    public async Task<List<GetAllLocationsDto>> GetAllLocationsAsync()
    {
      return await locationRepository.Query()
        .Where(x => !x.IsDeleted)
        .Select(x => new GetAllLocationsDto
        {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
        })
        .ToListAsync();
    }

    public async Task<GetByIdLocationDto> GetByIdLocationAsync(int id)
    {
      var foundLocation = await locationRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundLocation != null)
      {
        return new GetByIdLocationDto
        {
          Id = foundLocation.Id,
          Name = foundLocation.Name,
          Description = foundLocation.Description
        };
      }

      return null;
    }

    public async Task UpdateLocationAsync(UpdateLocationDto updateLocationDto)
    {
      var foundLocation = await locationRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == updateLocationDto.Id && !x.IsDeleted);

      if (foundLocation != null)
      {
        foundLocation.Name = updateLocationDto.Name;
        foundLocation.Description = updateLocationDto.Description;
        await unitOfWork.CommitAsync();
      }
    }
  }
}
