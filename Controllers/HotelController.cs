using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_voyager.Models;

namespace web_voyager.Controllers;

public class HotelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
