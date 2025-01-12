using IEvent.Services.EventServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEvent.Services.EventServices
{
  public class EventService : IEventService
  {
    public Task AddEventAsync(AddEventDto addEventDto)
    {
      throw new NotImplementedException();
    }

    public Task DeleteByIdEventAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<List<GetAllEventsDto>> GetAllEventsAsync()
    {
      throw new NotImplementedException();
    }

    public Task<GetByIdEventDto> GetByIdEventAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task UpdateEventAsync(UpdateEventDto updateEventDto)
    {
      throw new NotImplementedException();
    }
  }
}
