using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FerryBookingClassLibrary.Models
{
    public class Ferry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxCars { get; set; }
        public int MaxGuests { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal PricePerGuest { get; set; } = 99m;
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
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

