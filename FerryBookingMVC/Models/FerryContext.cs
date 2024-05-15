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
                .Property(f => f.CarPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ferry>()
                .Property(f => f.GuestPrice)
                .HasColumnType("decimal(18,2)");

            // Configure relationships
            modelBuilder.Entity<Guest>()
                .HasOne<Car>()
                .WithMany(c => c.Guests)
                .HasForeignKey(g => g.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Guest>()
                .HasOne<Ferry>()
                .WithMany(f => f.Guests)
                .HasForeignKey(g => g.FerryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne<Ferry>()
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FerryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<Ferry>().HasData(
                new Ferry
                {
                    Id = 1,
                    MaxCars = 100,
                    MaxGuests = 50,
                    CarPrice = 197,
                    GuestPrice = 99
                },
                new Ferry
                {
                    Id = 2,
                    MaxCars = 120,
                    MaxGuests = 60,
                    CarPrice = 197,
                    GuestPrice = 99
                }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, FerryId = 1 },
                new Car { Id = 2, FerryId = 1 },
                new Car { Id = 3, FerryId = 2 },
                new Car { Id = 4, FerryId = 2 }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "Alice Smith", Gender = "Female", CarId = 1, FerryId = 1 },
                new Guest { Id = 2, Name = "Bob Johnson", Gender = "Male", CarId = 1, FerryId = 1 },
                new Guest { Id = 3, Name = "Charlie Brown", Gender = "Male", CarId = 2, FerryId = 1 },
                new Guest { Id = 4, Name = "Diana Prince", Gender = "Female", CarId = 2, FerryId = 1 },
                new Guest { Id = 5, Name = "Eve Davis", Gender = "Female", CarId = 3, FerryId = 2 },
                new Guest { Id = 6, Name = "Frank Miller", Gender = "Male", CarId = 3, FerryId = 2 },
                new Guest { Id = 7, Name = "Grace Lee", Gender = "Female", CarId = 4, FerryId = 2 },
                new Guest { Id = 8, Name = "Hank Green", Gender = "Male", CarId = 4, FerryId = 2 }
            );
        }
    }
}