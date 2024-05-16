using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerryBookingClassLibrary;
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
        public async Task<IActionResult> Details(int? id)
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

        // GET: Cars/Create
        public IActionResult Create()
        {
            var ferries = _context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name");

            var guests = _context.Guests.ToList();
            ViewBag.Guests = guests;
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedGuestIds,FerryId")] CarViewModel carViewModel)
        {
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

            var guests = _context.Guests.ToList();
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

            var guests = _context.Guests.ToList();
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

            var guests = _context.Guests.ToList();
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
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
