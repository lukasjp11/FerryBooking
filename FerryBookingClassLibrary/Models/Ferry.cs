using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FerryBookingClassLibrary.Models
{
    public class Ferry
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MaxCars { get; set; }

        [Required]
        public int MaxGuests { get; set; }

        [Required]
        public int PricePerGuest { get; set; } = 99;

        [Required]
        public int PricePerCar { get; set; } = 197;

        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Guest> Guests { get; set; } = new List<Guest>();

        public int CalculateTotalPrice()
        {
            int total = 0;
            total += Cars.Count * PricePerCar;
            total += Guests.Count * PricePerGuest;
            return total;
        }
    }
}