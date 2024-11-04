using IEvent.Services.UserServices.Dto;

namespace IEvent.Services.UserServices
{
  public interface IUserService
  {
    public Task AddUserAsync(UserDto user);
  }
}
