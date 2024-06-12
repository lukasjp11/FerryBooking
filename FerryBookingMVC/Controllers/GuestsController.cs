using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Controllers
{
    public class GuestsController(FerryContext context) : Controller
    {
        // GET: Guests
        public async Task<IActionResult> Index()
        {
            List<Guest> guests = await context.Guests.ToListAsync();
            return View(guests);
        }

        // GET: Guests/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Guest? guest = await context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            List<Ferry> ferries = context.Ferries.ToList();
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

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Guest? guest = await context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // POST: Guests/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,FerryId")] Guest guest)
        {
            if (id != guest.Id)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Guest? guest = await context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Guest? guest = await context.Guests.FindAsync(id);
            if (guest != null)
            {
                context.Guests.Remove(guest);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(int id)
        {
            return context.Guests.Any(e => e.Id == id);
        }
    }
}