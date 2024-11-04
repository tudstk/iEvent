using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.UserServices.Dto;

namespace IEvent.Services.UserServices
{
  public class UserService : IUserService
  {
    private readonly IRepository<TestEntity> userRepository;
    private readonly IUnitOfWork unitOfWork;

    public UserService(IRepository<TestEntity> userRepository, IUnitOfWork unitOfWork)
    {
      this.userRepository = userRepository;
      this.unitOfWork = unitOfWork;
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
