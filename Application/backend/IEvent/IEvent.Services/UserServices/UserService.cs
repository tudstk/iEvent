using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.UserServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.Extensions.Options;

namespace IEvent.Services.UserServices
{
  public class UserService : IUserService
  {
    private readonly IRepository<TestEntity> userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public UserService
      (IRepository<TestEntity> userRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.userRepository = userRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }

    public async Task AddUserAsync(UserDto user)
    {
      var newUser = new TestEntity
      {
        Name = user.Name,
        Description = user.Description,
      };

      await userRepository.AddAsync(newUser);
      await unitOfWork.CommitAsync();
    }
  }
}
