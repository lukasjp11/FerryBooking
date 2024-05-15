using FerryBookingClassLibrary;
using FerryBookingClassLibrary.Models;
using FerryBookingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FerryBookingMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FerryApiController : ControllerBase
    {
        private readonly FerryContext _context;

        public FerryApiController(FerryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ferry>>> GetFerries()
        {
            return await _context.Ferries.Include(f => f.Cars).Include(f => f.Guests).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ferry>> GetFerry(int id)
        {
            var ferry = await _context.Ferries.Include(f => f.Cars).Include(f => f.Guests)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            return ferry;
        }

        [HttpGet("cars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(c => c.Guests).ToListAsync();
        }

        [HttpGet("cars/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Include(c => c.Guests)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet("guests")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        [HttpGet("guests/{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }
    }
}
