using FerryBookingClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FerryBookingMVC.Models
{
    public class FerryContext : DbContext
    {
        public FerryContext(DbContextOptions<FerryContext> options) : base(options)
        {
        }

        public DbSet<Ferry> Ferries { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Guest> Guests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify precision and scale for decimal properties
            modelBuilder.Entity<Ferry>()
                .Property(f => f.PricePerCar)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ferry>()
                .Property(f => f.PricePerGuest)
                .HasColumnType("decimal(18,2)");

            // Configure relationships
            modelBuilder.Entity<Guest>()
                .HasOne(g => g.Car)
                .WithMany(c => c.Guests)
                .HasForeignKey(g => g.CarId)
                .OnDelete(DeleteBehavior.Cascade);

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
                    Name = "Ferry 1",
                    MaxCars = 100,
                    MaxGuests = 50,
                    PricePerCar = 197,
                    PricePerGuest = 99
                },
                new Ferry
                {
                    Id = 2,
                    Name = "Ferry 2",
                    MaxCars = 120,
                    MaxGuests = 60,
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
                new Guest { Id = 1, Name = "Alice Smith", Gender = true, CarId = 1, FerryId = 1 },
                new Guest { Id = 2, Name = "Bob Johnson", Gender = false, CarId = 1, FerryId = 1 },
                new Guest { Id = 3, Name = "Charlie Brown", Gender = false, CarId = 2, FerryId = 1 },
                new Guest { Id = 4, Name = "Diana Prince", Gender = true, CarId = 2, FerryId = 1 },
                new Guest { Id = 5, Name = "Eve Davis", Gender = true, CarId = 3, FerryId = 2 },
                new Guest { Id = 6, Name = "Frank Miller", Gender = false, CarId = 3, FerryId = 2 },
                new Guest { Id = 7, Name = "Grace Lee", Gender = true, CarId = 4, FerryId = 2 },
                new Guest { Id = 8, Name = "Hank Green", Gender = false, CarId = 4, FerryId = 2 },
                new Guest { Id = 9, Name = "Isaac Newton", Gender = false, CarId = null, FerryId = 1 },
                new Guest { Id = 10, Name = "Marie Curie", Gender = true, CarId = null, FerryId = 1 }
            );
        }
    }
}
