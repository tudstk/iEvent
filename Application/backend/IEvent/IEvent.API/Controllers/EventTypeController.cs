using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  public class EventTypeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
