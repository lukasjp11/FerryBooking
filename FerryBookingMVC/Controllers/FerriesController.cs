using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerryBookingClassLibrary;
using FerryBookingClassLibrary.Models;
using FerryBookingMVC.Models;

namespace FerryBookingMVC.Controllers
{
    public class FerriesController : Controller
    {
        private readonly FerryContext _context;

        public FerriesController(FerryContext context)
        {
            _context = context;
        }

        // GET: Ferries
        public async Task<IActionResult> Index()
        {
            var ferries = await _context.Ferries
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .ToListAsync();
            return View(ferries);
        }

        // GET: Ferries/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ferry == null)
                return NotFound();

            return View(ferry);
        }

        // GET: Ferries/Create
        public IActionResult Create() => View();

        // POST: Ferries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaxCars,MaxGuests,PricePerGuest,PricePerCar")] Ferry ferry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ferry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ferry);
        }

        // GET: Ferries/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ferry = await _context.Ferries.FindAsync(id);
            if (ferry == null)
                return NotFound();

            return View(ferry);
        }

        // POST: Ferries/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MaxCars,MaxGuests,PricePerGuest,PricePerCar")] Ferry ferry)
        {
            if (id != ferry.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ferry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FerryExists(ferry.Id))
                        return NotFound();

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ferry);
        }

        // GET: Ferries/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var ferry = await _context.Ferries
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ferry == null)
                return NotFound();

            return View(ferry);
        }

        // POST: Ferries/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ferry = await _context.Ferries.FindAsync(id);
            if (ferry != null)
            {
                _context.Ferries.Remove(ferry);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FerryExists(int id) => _context.Ferries.Any(e => e.Id == id);
    }
}
