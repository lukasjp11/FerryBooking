using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerryBookingClassLibrary;
using FerryBookingClassLibrary.Models;
using FerryBookingMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace FerryBookingMVC.Controllers
{
    public class GuestsController(FerryContext context) : Controller
    {
        // GET: Guests
        public async Task<IActionResult> Index()
        {
            var guests = await context.Guests.ToListAsync();
            return View(guests);
        }

        // GET: Guests/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            var ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name");
            return View();
        }

        // POST: Guests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Gender,FerryId")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                context.Add(guest);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await context.Guests.FindAsync(id);
            if (guest == null)
                return NotFound();

            var ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // POST: Guests/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,FerryId")] Guest guest)
        {
            if (id != guest.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(guest);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.Id))
                        return NotFound();
                    throw;
                }
            }

            var ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // POST: Guests/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await context.Guests.FindAsync(id);
            if (guest != null)
            {
                context.Guests.Remove(guest);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(int id) => context.Guests.Any(e => e.Id == id);
    }
}
