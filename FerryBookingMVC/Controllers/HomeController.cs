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
    }
}