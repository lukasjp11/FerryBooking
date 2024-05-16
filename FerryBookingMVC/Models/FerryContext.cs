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

            // Configure relationships
            modelBuilder.Entity<Guest>()
                .HasOne(g => g.Ferry)
                .WithMany(f => f.Guests)
                .HasForeignKey(g => g.FerryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Ferry)
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FerryId)
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

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, FerryId = 1 },
                new Car { Id = 2, FerryId = 1 },
                new Car { Id = 3, FerryId = 2 },
                new Car { Id = 4, FerryId = 2 }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "Alice Smith", Gender = true, FerryId = 1 },
                new Guest { Id = 2, Name = "Bob Johnson", Gender = false, FerryId = 1 },
                new Guest { Id = 3, Name = "Charlie Brown", Gender = false, FerryId = 1 },
                new Guest { Id = 4, Name = "Diana Prince", Gender = true, FerryId = 1 },
                new Guest { Id = 5, Name = "Eve Davis", Gender = true, FerryId = 2 },
                new Guest { Id = 6, Name = "Frank Miller", Gender = false, FerryId = 2 },
                new Guest { Id = 7, Name = "Grace Lee", Gender = true, FerryId = 2 },
                new Guest { Id = 8, Name = "Hank Green", Gender = false, FerryId = 2 },
                new Guest { Id = 9, Name = "Isaac Newton", Gender = false, FerryId = 1 },
                new Guest { Id = 10, Name = "Marie Curie", Gender = true, FerryId = 1 }
            );
        }
    }
}
