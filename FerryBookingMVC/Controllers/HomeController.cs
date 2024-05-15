using FerryBookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly FerryContext _context;

        public HomeController(FerryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ferries = await _context.Ferries.Include(f => f.Cars).Include(f => f.Guests).ToListAsync();
            return View(ferries);
        }
    }
}