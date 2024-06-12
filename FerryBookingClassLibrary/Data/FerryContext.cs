using FerryBookingClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FerryBookingClassLibrary.Data
{
    public class FerryContext : DbContext
    {
        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Guest> Guests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost,1433;Database=FerryDB;User Id=sa;Password=Rbj93zpj!;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many relationship between Ferry and Car with cascade delete
            modelBuilder.Entity<Ferry>()
                .HasMany(f => f.Cars)
                .WithOne(c => c.Ferry)
                .HasForeignKey(c => c.FerryId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship between Ferry and Guest with cascade delete
            modelBuilder.Entity<Ferry>()
                .HasMany(f => f.Guests)
                .WithOne(g => g.Ferry)
                .HasForeignKey(g => g.FerryId)
                .OnDelete(DeleteBehavior.Cascade);

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
            List<EntityEntry> deletedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && (e.Entity is Ferry || e.Entity is Car))
                .ToList();

            foreach (EntityEntry entry in deletedEntries)
            {
                if (entry.Entity is Ferry ferry)
                {
                    List<Guest> guests = Guests.Where(g => g.FerryId == ferry.Id).ToList();
                    Guests.RemoveRange(guests);

                    List<Car> cars = Cars.Where(c => c.FerryId == ferry.Id).ToList();
                    foreach (Car car in cars)
                    {
                        List<Guest> carGuests = Guests.Where(g => g.FerryId == car.FerryId).ToList();
                        Guests.RemoveRange(carGuests);
                    }

                    Cars.RemoveRange(cars);
                }
                else if (entry.Entity is Car car)
                {
                    List<Guest> guests = Guests.Where(g => g.FerryId == car.FerryId).ToList();
                    Guests.RemoveRange(guests);
                }
            }

            return base.SaveChanges();
        }
    }
}