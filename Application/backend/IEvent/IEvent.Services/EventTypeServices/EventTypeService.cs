using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.EventTypeServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.EventTypeServices
{
  public class EventTypeService : IEventTypeService
  {
    private readonly IRepository<EventType> eventTypeRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public EventTypeService
      (IRepository<EventType> eventTypeRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.eventTypeRepository = eventTypeRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }
    public async Task AddEventTypeAsync(AddEventTypeDto addEventTypeDto)
    {
      var newEventType = new EventType
      {
        Name = addEventTypeDto.Name,
        Description = addEventTypeDto.Description,
        IsDeleted = false
      };

      await eventTypeRepository.AddAsync(newEventType);
      await unitOfWork.CommitAsync();
    }

    public async Task DeleteByIdEventTypeAsync(int id)
    {
      var foundEventType = await eventTypeRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundEventType != null)
      {
        foundEventType.IsDeleted = true;
        await unitOfWork.CommitAsync();
      }
    }

    public async Task<List<GetAllEventTypesDto>> GetAllEventTypesAsync()
    {
      return await eventTypeRepository.Query()
        .Where(x => !x.IsDeleted)
        .Select(x => new GetAllEventTypesDto
        {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
        })
        .ToListAsync();
    }

    public async Task<GetByIdEventTypeDto> GetByIdEventTypeAsync(int id)
    {
      var foundEventType = await eventTypeRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

      if (foundEventType != null)
      {
        return new GetByIdEventTypeDto
        {
          Id = foundEventType.Id,
          Name = foundEventType.Name,
          Description = foundEventType.Description
        };
      }

      return null;
    }

    public async Task UpdateEventTypeAsync(UpdateEventTypeDto updateEventTypeDto)
    {
      var foundEventType = await eventTypeRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == updateEventTypeDto.Id && !x.IsDeleted);

      if (foundEventType != null)
      {
        foundEventType.Name = updateEventTypeDto.Name;
        foundEventType.Description = updateEventTypeDto.Description;
        await unitOfWork.CommitAsync();
      }
    }
  }
}
