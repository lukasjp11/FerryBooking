using FerryBookingClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Models
{
    public class FerryContext : DbContext
    {
        public FerryContext(DbContextOptions<FerryContext> options) : base(options) { }

        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Guest> Guests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many relationship between Ferry and Car
            modelBuilder.Entity<Ferry>()
                .HasMany(f => f.Cars)
                .WithOne(c => c.Ferry)
                .HasForeignKey(c => c.FerryId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many relationship between Ferry and Guest
            modelBuilder.Entity<Ferry>()
                .HasMany(f => f.Guests)
                .WithOne(g => g.Ferry)
                .HasForeignKey(g => g.FerryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            modelBuilder.Entity<Ferry>().HasData(
                new Ferry
                {
                    Id = 1,
                    Name = "MOLSLINJEN (Express 4)",
                    MaxCars = 400,
                    MaxGuests = 980,
                    PricePerCar = 249,
                    PricePerGuest = 149
                },
                new Ferry
                {
                    Id = 2,
                    Name = "Standard Ferry",
                    MaxCars = 50,
                    MaxGuests = 100,
                    PricePerCar = 197,
                    PricePerGuest = 99
                }
            );
        }

        public override int SaveChanges()
        {
            var deletedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is Ferry || e.Entity is Car)
                .ToList();

            foreach (var entry in deletedEntries)
            {
                if (entry.Entity is Ferry ferry)
                {
                    // Manually delete related guests
                    var guests = Guests.Where(g => g.FerryId == ferry.Id).ToList();
                    Guests.RemoveRange(guests);

                    // Manually delete related cars
                    var cars = Cars.Where(c => c.FerryId == ferry.Id).ToList();
                    foreach (var car in cars)
                    {
                        // Manually delete guests related to each car
                        var carGuests = Guests.Where(g => g.FerryId == car.FerryId).ToList();
                        Guests.RemoveRange(carGuests);
                    }
                    Cars.RemoveRange(cars);
                }
                else if (entry.Entity is Car car)
                {
                    // Manually delete related guests
                    var guests = Guests.Where(g => g.FerryId == car.FerryId).ToList();
                    Guests.RemoveRange(guests);
                }
            }

            return base.SaveChanges();
        }
    }
}
