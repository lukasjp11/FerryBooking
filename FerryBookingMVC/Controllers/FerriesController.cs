using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Controllers
{
    public class FerriesController(FerryContext context) : Controller
    {
        // GET: Ferries
        public async Task<IActionResult> Index()
        {
            List<Ferry> ferries = await context.Ferries
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
            {
                return NotFound();
            }

            Ferry? ferry = await context.Ferries
                .Include(f => f.Cars)
                .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ferry == null)
            {
                return NotFound();
            }

            return View(ferry);
        }

        // GET: Ferries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ferries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,MaxCars,MaxGuests,PricePerGuest,PricePerCar")] Ferry ferry)
        {
            if (ModelState.IsValid)
            {
                context.Add(ferry);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ferry);
        }

        // GET: Ferries/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ferry? ferry = await context.Ferries.FindAsync(id);
            if (ferry == null)
            {
                return NotFound();
            }

            return View(ferry);
        }

        // POST: Ferries/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,MaxCars,MaxGuests,PricePerGuest,PricePerCar")] Ferry ferry)
        {
            if (id != ferry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(ferry);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FerryExists(ferry.Id))
                    {
                        return NotFound();
                    }

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
            {
                return NotFound();
            }

            Ferry? ferry = await context.Ferries
                .Include(f => f.Cars)
                .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ferry == null)
            {
                return NotFound();
            }

            return View(ferry);
        }

        // POST: Ferries/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Ferry? ferry = await context.Ferries
                .Include(f => f.Cars)
                .ThenInclude(c => c.Guests)
                .Include(f => f.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ferry != null)
            {
                context.Ferries.Remove(ferry);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FerryExists(int id)
        {
            return context.Ferries.Any(e => e.Id == id);
        }
    }
}