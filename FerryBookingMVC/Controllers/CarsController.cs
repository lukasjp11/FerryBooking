using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerryBookingClassLibrary.Models;
using FerryBookingMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FerryBookingMVC.Controllers
{
    public class CarsController : Controller
    {
        private readonly FerryContext _context;

        public CarsController(FerryContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.Include(c => c.Guests).ToListAsync();
            return View(cars);
        }

        // GET: Cars/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Guests)
                .Include(c => c.Ferry)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var viewModel = new CarViewModel
            {
                Id = car.Id,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList(),
                GuestNames = car.Guests.Select(g => g.Name).ToList(),
                FerryId = car.FerryId,
                FerryName = car.Ferry?.Name
            };

            return View(viewModel);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name");

            // Initially, show guests for the first ferry or no guests if no ferry is selected
            var firstFerryId = ferries.FirstOrDefault()?.Id ?? 0;
            var guests = _context.Guests
                .Where(g => g.FerryId == firstFerryId && !_context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
                .ToList();
            ViewBag.Guests = guests;
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedGuestIds,FerryId")] CarViewModel carViewModel)
        {
            if (carViewModel.SelectedGuestIds.Count < 1 || carViewModel.SelectedGuestIds.Count > 5)
            {
                ModelState.AddModelError("SelectedGuestIds", "The car must have at least 1 guest and a maximum of 5 guests.");
            }

            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    FerryId = carViewModel.FerryId,
                    Guests = _context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList()
                };

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", carViewModel.FerryId);

            var guests = _context.Guests
                .Where(g => g.FerryId == carViewModel.FerryId && !_context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
                .ToList();
            ViewBag.Guests = guests;
            return View(carViewModel);
        }

        // GET: Cars/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", car.FerryId);

            var guests = _context.Guests
                .Where(g => g.FerryId == car.FerryId && (!_context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)) || car.Guests.Select(g => g.Id).Contains(g.Id)))
                .ToList();
            ViewBag.Guests = guests;

            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                FerryId = car.FerryId,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
            };

            return View(carViewModel);
        }

        // POST: Cars/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SelectedGuestIds,FerryId")] CarViewModel carViewModel)
        {
            if (id != carViewModel.Id)
            {
                return NotFound();
            }

            if (carViewModel.SelectedGuestIds.Count < 1 || carViewModel.SelectedGuestIds.Count > 5)
            {
                ModelState.AddModelError("SelectedGuestIds", "The car must have at least 1 guest and a maximum of 5 guests.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var car = await _context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
                    if (car == null)
                    {
                        return NotFound();
                    }

                    car.FerryId = carViewModel.FerryId;
                    car.Guests = _context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList();

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carViewModel.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", carViewModel.FerryId);

            var guests = _context.Guests
                .Where(g => g.FerryId == carViewModel.FerryId && !_context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
                .ToList();
            ViewBag.Guests = guests;
            return View(carViewModel);
        }

        // GET: Cars/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var guests = _context.Guests.ToList();
            ViewBag.Guests = guests;

            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                FerryId = car.FerryId,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList(),
                FerryName = _context.Ferries.FirstOrDefault(f => f.Id == car.FerryId)?.Name,
                GuestNames = car.Guests.Select(g => g.Name).ToList()
            };

            return View(carViewModel);
        }

        // POST: Cars/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
            if (car != null)
            {
                // Delete all guests related to this car
                var guests = car.Guests.ToList();
                _context.Guests.RemoveRange(guests);

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestsByFerry(int ferryId, int carId)
        {
            var guests = await _context.Guests
                .Where(g => g.FerryId == ferryId)
                .ToListAsync();

            var carGuests = await _context.Cars
                .Where(c => c.Id == carId)
                .SelectMany(c => c.Guests)
                .ToListAsync();

            guests.AddRange(carGuests.Where(g => g.FerryId == ferryId && !guests.Any(existingGuest => existingGuest.Id == g.Id)));

            return Json(guests.Select(g => new { g.Id, g.Name }));
        }
    }
}
