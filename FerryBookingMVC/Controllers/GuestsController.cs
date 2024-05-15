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
    public class GuestsController : Controller
    {
        private readonly FerryContext _context;
        private readonly ILogger<GuestsController> _logger;

        public GuestsController(FerryContext context, ILogger<GuestsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Guests
        public async Task<IActionResult> Index()
        {
            var guests = await _context.Guests.ToListAsync();
            return View(guests);
        }

        // GET: Guests/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name");
            return View();
        }

        // POST: Guests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Gender,FerryId")] Guest guest)
        {
            _logger.LogInformation($"Received FerryId: {guest.FerryId}");

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model state is valid. Adding guest to context.");
                _context.Add(guest);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Guest saved successfully. Redirecting to Index.");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogWarning("Model state is invalid. Returning to Create view.");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogWarning($"Key: {key}, Error: {error.ErrorMessage}");
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }

            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
                return NotFound();

            var ferries = _context.Ferries.ToList();
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
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.Id))
                        return NotFound();
                    throw;
                }
            }

            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", guest.FerryId);
            return View(guest);
        }

        // GET: Guests/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);
            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // POST: Guests/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(int id) => _context.Guests.Any(e => e.Id == id);
    }
}
