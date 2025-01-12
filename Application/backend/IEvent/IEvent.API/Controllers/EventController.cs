using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  public class EventController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
