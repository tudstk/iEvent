using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.EventServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.EventServices
{
  public class EventService : IEventService
  {
    private readonly IRepository<Event> eventRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public EventService
      (IRepository<Event> eventRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.eventRepository = eventRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }
    public async Task AddEventAsync(AddEventDto addEventDto)
    {
      var newEvent = new Event
      {
        Name = addEventDto.Name,
        Description = addEventDto.Description,
        IsDeleted = false,
        Date = addEventDto.Date,
        LocationId = addEventDto.LocationId,
        EventTypeId = addEventDto.EventTypeId,
        GenreId = addEventDto.GenreId,
        MainArtistId = addEventDto.MainArtistId,
        Theme = addEventDto.Theme,
      };

      await eventRepository.AddAsync(newEvent);
      await unitOfWork.CommitAsync();
    }

    public async Task DeleteByIdEventAsync(int id)
    {
      var foundEvent = await eventRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundEvent != null)
      {
        foundEvent.IsDeleted = true;
        await unitOfWork.CommitAsync();
      }
    }

    public async Task<List<GetAllEventsDto>> GetAllEventsAsync()
    {
      return await eventRepository.Query()
        .Where(x => !x.IsDeleted)
        .Select(x => new GetAllEventsDto
        {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
          Date = x.Date,
          LocationName = x.Location.Name,
          EventTypeName = x.EventType.Name,
          GenreName = x.Genre.Name,
          MainArtistName = x.MainArtist.Name,
          Theme = x.Theme,
        })
        .ToListAsync();
    }

    public async Task<GetByIdEventDto> GetByIdEventAsync(int id)
    {
      var foundEvent = await eventRepository.Query()
        .Include(x => x.Location)
        .Include(x => x.EventType)
        .Include(x => x.Genre)
        .Include(x => x.MainArtist)
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundEvent != null)
      {
        return new GetByIdEventDto
        {
          Id = foundEvent.Id,
          Name = foundEvent.Name,
          Description = foundEvent.Description,
          Date = foundEvent.Date,
          LocationName = foundEvent.Location.Name,
          EventTypeName = foundEvent.EventType.Name,
          GenreName = foundEvent.Genre.Name,
          MainArtistName = foundEvent.MainArtist.Name,
          Theme = foundEvent.Theme,
        };
      }

      return null;
    }

    public async Task UpdateEventAsync(UpdateEventDto updateEventDto)
    {
      var foundEvent = await eventRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == updateEventDto.Id && !x.IsDeleted);

      if (foundEvent != null)
      {
        foundEvent.Name = updateEventDto.Name;
        foundEvent.Description = updateEventDto.Description;
        foundEvent.Date = updateEventDto.Date;
        foundEvent.LocationId = updateEventDto.LocationId;
        foundEvent.EventTypeId = updateEventDto.EventTypeId;
        foundEvent.GenreId = updateEventDto.GenreId;
        foundEvent.MainArtistId = updateEventDto.MainArtistId;
        foundEvent.Theme = updateEventDto.Theme;
        await unitOfWork.CommitAsync();
      }
    }
  }
}
