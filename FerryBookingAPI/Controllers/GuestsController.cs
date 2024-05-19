using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerryBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly FerryContext _context;

        public GuestsController(FerryContext context)
        {
            _context = context;
        }

        // GET: api/Guests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        // GET: api/Guests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(int id)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }

        // PUT: api/Guests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(int id, Guest guest)
        {
            if (id != guest.Id)
            {
                return BadRequest();
            }

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // POST: api/Guests
        [HttpPost]
        public async Task<ActionResult<Guest>> PostGuest(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGuest), new { id = guest.Id }, guest);
        }

        // DELETE: api/Guests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.Id == id);
        }

        [HttpGet("byferry/{ferryId}")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuestsByFerry(int ferryId)
        {
            return await _context.Guests.Where(g => g.FerryId == ferryId).ToListAsync();
        }
    }
}
