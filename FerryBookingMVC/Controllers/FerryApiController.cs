using FerryBookingClassLibrary;
using FerryBookingClassLibrary.Models;
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
            return await _context.Ferries.ToListAsync();
        }

        // GET: api/FerryApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ferry>> GetFerry(int id)
        {
            var ferry = await _context.Ferries.FindAsync(id);

            if (ferry == null)
            {
                return NotFound();
            }

            return ferry;
        }

        // POST: api/FerryApi
        [HttpPost]
        public async Task<ActionResult<Ferry>> PostFerry(Ferry ferry)
        {
            _context.Ferries.Add(ferry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFerry), new { id = ferry.Id }, ferry);
        }

        // PUT: api/FerryApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFerry(int id, Ferry ferry)
        {
            if (id != ferry.Id)
            {
                return BadRequest();
            }

            _context.Entry(ferry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FerryExists(id))
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

        // DELETE: api/FerryApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFerry(int id)
        {
            var ferry = await _context.Ferries.FindAsync(id);
            if (ferry == null)
            {
                return NotFound();
            }

            _context.Ferries.Remove(ferry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/FerryApi/5/cars
        [HttpGet("{id}/cars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetFerryCars(int id)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            return ferry.Cars;
        }

        // POST: api/FerryApi/5/cars
        [HttpPost("{id}/cars")]
        public async Task<ActionResult<Car>> PostCarToFerry(int id, Car car)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            ferry.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFerryCars), new { id = ferry.Id }, car);
        }

        // DELETE: api/FerryApi/5/cars/10
        [HttpDelete("{id}/cars/{carId}")]
        public async Task<IActionResult> DeleteCarFromFerry(int id, int carId)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            var car = ferry.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                return NotFound();
            }

            ferry.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/FerryApi/5/guests
        [HttpGet("{id}/guests")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetFerryGuests(int id)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            return ferry.Guests;
        }

        // POST: api/FerryApi/5/guests
        [HttpPost("{id}/guests")]
        public async Task<ActionResult<Guest>> PostGuestToFerry(int id, Guest guest)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            ferry.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFerryGuests), new { id = ferry.Id }, guest);
        }

        // DELETE: api/FerryApi/5/guests/10
        [HttpDelete("{id}/guests/{guestId}")]
        public async Task<IActionResult> DeleteGuestFromFerry(int id, int guestId)
        {
            var ferry = await _context.Ferries
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            var guest = ferry.Guests.FirstOrDefault(g => g.Id == guestId);
            if (guest == null)
            {
                return NotFound();
            }

            ferry.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FerryExists(int id)
        {
            return _context.Ferries.Any(e => e.Id == id);
        }
    }
}
