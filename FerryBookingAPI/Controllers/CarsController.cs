using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using FerryBookingClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerryBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly FerryContext _context;

        public CarsController(FerryContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(c => c.Guests).ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Guests)
                .Include(c => c.Ferry)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarViewModel carViewModel)
        {
            if (id != carViewModel.Id)
            {
                return BadRequest();
            }

            if (carViewModel.SelectedGuestIds.Count < 1 || carViewModel.SelectedGuestIds.Count > 5)
            {
                ModelState.AddModelError("SelectedGuestIds", "The car must have at least 1 guest and a maximum of 5 guests.");
                return BadRequest(ModelState);
            }

            var car = await _context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            car.FerryId = carViewModel.FerryId;
            car.Guests = _context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList();

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
