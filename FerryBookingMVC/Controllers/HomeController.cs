using Microsoft.AspNetCore.Mvc;

namespace FerryBookingMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}