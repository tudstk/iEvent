using IEvent.Services.UserServices.Dto;

namespace IEvent.Services.UserServices
{
  public interface IUserService
  {
    public Task UpdateProfileAsync(int personId, ModifyProfileDto modifyProfileDto);

    public Task<GetProfileDto> GetProfileAsync(int personId);

    public Task AddEventForUser(int personId, int eventId);

    public Task RemoveEventForUser(int personId, int eventId);

    public Task<List<UserEventDto>> GetRecommendedUserEvents(int personId);

    public Task<List<UserEventDto>> GetUserEvents(int personId);
  }
}
