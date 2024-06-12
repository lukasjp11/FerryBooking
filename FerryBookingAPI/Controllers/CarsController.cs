using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using FerryBookingClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/Cars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            Car? car = await _context.Cars
                .Include(c => c.Guests)
                .Include(c => c.Ferry)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarViewModel carViewModel)
        {
            if (carViewModel.SelectedGuestIds.Count < 1 || carViewModel.SelectedGuestIds.Count > 5)
            {
                ModelState.AddModelError("SelectedGuestIds",
                    "The car must have at least 1 guest and a maximum of 5 guests.");
                return BadRequest(ModelState);
            }

            Car car = new Car
            {
                FerryId = carViewModel.FerryId,
                Guests = _context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList()
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        // PUT: api/Cars/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarViewModel carViewModel)
        {
            if (id != carViewModel.Id)
            {
                return BadRequest();
            }

            if (carViewModel.SelectedGuestIds.Count < 1 || carViewModel.SelectedGuestIds.Count > 5)
            {
                ModelState.AddModelError("SelectedGuestIds",
                    "The car must have at least 1 guest and a maximum of 5 guests.");
                return BadRequest(ModelState);
            }

            Car? car = await _context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
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

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Cars/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            Car? car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}