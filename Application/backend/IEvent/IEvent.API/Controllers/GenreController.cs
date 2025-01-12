using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  public class GenreController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
