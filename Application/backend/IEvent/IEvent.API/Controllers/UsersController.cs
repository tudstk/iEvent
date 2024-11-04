using IEvent.API.Models.UserModels;
using IEvent.Services.UserServices;
using IEvent.Services.UserServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
      this.userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public IActionResult Get()
    {
      var users = new List<object>
            {
                new
                {
                    Name = "Andrei",
                    Age = 25,
                    Email = "some_email@gmail.com"
                },
                new
                {
                    Name = "Rady",
                    Age = 30,
                    Email = "other_email@gmail.com"
                }
            };
      return new JsonResult(users);
    }

    [HttpPost]
    public IActionResult Post(UserModel model)
    {
      var userDto = new UserDto
      {
        Name = model.Name,
        Description = model.Description,
      };

      userService.AddUserAsync(userDto);

      return StatusCode(201);
    }
  }
}
