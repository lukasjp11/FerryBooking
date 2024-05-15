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

            // Seeding Ferry data
            modelBuilder.Entity<Ferry>().HasData(
                new Ferry { Id = 1, Length = 10, MaxGuests = 40, GuestPrice = 99, CarPrice = 197 },
                new Ferry { Id = 2, Length = 20, MaxGuests = 80, GuestPrice = 99, CarPrice = 197 }
            );
        }
    }
}