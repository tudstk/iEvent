﻿using Microsoft.AspNetCore.Mvc;

namespace IEvent.API.Controllers
{
  public class LocationsController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
