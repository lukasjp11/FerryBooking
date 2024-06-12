using FerryBookingClassLibrary.Data;
using FerryBookingClassLibrary.Models;
using FerryBookingClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Controllers
{
    public class CarsController(FerryContext context) : Controller
    {
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            List<Car> cars = await context.Cars.Include(c => c.Guests).ToListAsync();
            return View(cars);
        }

        // GET: Cars/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            Car? car = await context.Cars
                .Include(c => c.Guests)
                .Include(c => c.Ferry)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            CarViewModel viewModel = new CarViewModel
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
            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name");

            int firstFerryId = ferries.FirstOrDefault()?.Id ?? 0;
            List<Guest> guests = context.Guests
                .Where(g => g.FerryId == firstFerryId && !context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
                .ToList();
            ViewBag.Guests = guests;
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SelectedGuestIds,FerryId")] CarViewModel carViewModel)
        {
            if (carViewModel.SelectedGuestIds.Count is < 1 or > 5)
            {
                ModelState.AddModelError("SelectedGuestIds",
                    "The car must have at least 1 guest and a maximum of 5 guests.");
            }

            if (ModelState.IsValid)
            {
                Car car = new Car
                {
                    FerryId = carViewModel.FerryId,
                    Guests = context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList()
                };

                context.Add(car);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", carViewModel.FerryId);

            List<Guest> guests = context.Guests
                .Where(g => g.FerryId == carViewModel.FerryId &&
                            !context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
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

            Car? car = await context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", car.FerryId);

            List<Guest> guests = context.Guests
                .Where(g => g.FerryId == car.FerryId && (!context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)) ||
                                                         car.Guests.Select(g => g.Id).Contains(g.Id)))
                .ToList();
            ViewBag.Guests = guests;

            CarViewModel carViewModel = new CarViewModel
            {
                Id = car.Id, FerryId = car.FerryId, SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
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
                ModelState.AddModelError("SelectedGuestIds",
                    "The car must have at least 1 guest and a maximum of 5 guests.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Car? car = await context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
                    if (car == null)
                    {
                        return NotFound();
                    }

                    car.FerryId = carViewModel.FerryId;
                    car.Guests = context.Guests.Where(g => carViewModel.SelectedGuestIds.Contains(g.Id)).ToList();

                    context.Update(car);
                    await context.SaveChangesAsync();
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

            List<Ferry> ferries = context.Ferries.ToList();
            ViewBag.Ferries = new SelectList(ferries, "Id", "Name", carViewModel.FerryId);

            List<Guest> guests = context.Guests
                .Where(g => g.FerryId == carViewModel.FerryId &&
                            !context.Cars.Any(c => c.Guests.Any(cg => cg.Id == g.Id)))
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

            Car? car = await context.Cars
                .Include(c => c.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            List<Guest> guests = context.Guests.ToList();
            ViewBag.Guests = guests;

            CarViewModel carViewModel = new CarViewModel
            {
                Id = car.Id,
                FerryId = car.FerryId,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList(),
                FerryName = context.Ferries.FirstOrDefault(f => f.Id == car.FerryId)?.Name,
                GuestNames = car.Guests.Select(g => g.Name).ToList()
            };

            return View(carViewModel);
        }

        // POST: Cars/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Car? car = await context.Cars.Include(c => c.Guests).FirstOrDefaultAsync(m => m.Id == id);
            if (car != null)
            {
                context.Cars.Remove(car);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return context.Cars.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestsByFerry(int ferryId, int carId)
        {
            List<Guest> guests = await context.Guests
                .Where(g => g.FerryId == ferryId &&
                            !context.Cars.Any(c => c.Id != carId && c.Guests.Any(cg => cg.Id == g.Id)))
                .ToListAsync();

            List<Guest> carGuests = await context.Cars
                .Where(c => c.Id == carId)
                .SelectMany(c => c.Guests)
                .ToListAsync();

            guests.AddRange(carGuests.Where(g =>
                g.FerryId == ferryId && !guests.Any(existingGuest => existingGuest.Id == g.Id)));
            return Json(guests.Select(g => new { g.Id, g.Name }));
        }
    }
}