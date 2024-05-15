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

        // GET: api/FerryApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ferry>>> GetFerries()
        {
            var ferries = await _context.Ferries
                .Include(f => f.Cars)
                .ThenInclude(c => c.Guests)
                .ToListAsync();
            return Ok(ferries);
        }

        // GET: api/FerryApi/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Ferry>> GetFerry(int id)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                .ThenInclude(c => c.Guests)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
                return NotFound();

            return Ok(ferry);
        }

        // GET: api/FerryApi/cars
        [HttpGet("cars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _context.Cars
                .Include(c => c.Guests)
                .ToListAsync();
            return Ok(cars);
        }

        // GET: api/FerryApi/cars/{id}
        [HttpGet("cars/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Guests)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // GET: api/FerryApi/guests
        [HttpGet("guests")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
        {
            var guests = await _context.Guests.ToListAsync();
            return Ok(guests);
        }

        // GET: api/FerryApi/guests/{id}
        [HttpGet("guests/{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
                return NotFound();

            return Ok(guest);
        }
    }
}
