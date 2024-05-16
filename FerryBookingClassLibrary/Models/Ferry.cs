using System;
using System.Collections.Generic;
using System.Linq;

namespace FerryBookingClassLibrary.Models
{
    public class Ferry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxCars { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerGuest { get; set; } = 99m;
        public decimal PricePerCar { get; set; } = 197m;
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Guest> Guests { get; set; } = new List<Guest>();

        public decimal CalculateTotalPrice()
        {
            decimal total = 0;
            total += Cars.Count * PricePerCar;
            total += Guests.Count * PricePerGuest;
            return total;
        }
    }
}

