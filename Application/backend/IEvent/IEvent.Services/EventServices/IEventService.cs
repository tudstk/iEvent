using IEvent.Services.EventServices.Dto;

namespace IEvent.Services.EventServices
{
  public interface IEventService
  {
    public Task AddEventAsync(AddEventDto addEventDto);

    public Task UpdateEventAsync(UpdateEventDto updateEventDto);

    public Task<List<GetAllEventsDto>> GetAllEventsAsync();

    public Task<GetByIdEventDto> GetByIdEventAsync(int id);

    public Task DeleteByIdEventAsync(int id);
  }
}
