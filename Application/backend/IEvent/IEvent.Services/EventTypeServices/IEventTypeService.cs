using IEvent.Services.EventTypeServices.Dto;

namespace IEvent.Services.EventTypeServices
{
  public interface IEventTypeService
  {
    public Task AddEventTypeAsync(AddEventTypeDto addEventTypeDto);

    public Task UpdateEventTypeAsync(UpdateEventTypeDto updateEventTypeDto);

    public Task<List<GetAllEventTypesDto>> GetAllEventTypesAsync();

    public Task<GetByIdEventTypeDto> GetByIdEventTypeAsync(int id);

    public Task DeleteByIdEventTypeAsync(int id);
  }
}
