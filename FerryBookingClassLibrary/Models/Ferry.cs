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

        public void AddGuest(Guest guest)
        {
            if (Guests.Count >= MaxGuests)
                throw new InvalidOperationException("Cannot add more guests, ferry is full.");
            Guests.Add(guest);
        }

        public void RemoveGuest(Guest guest)
        {
            Guests.Remove(guest);
        }

        public void AddCar(Car car)
        {
            if (Cars.Count >= MaxCars)
                throw new InvalidOperationException("Cannot add more cars, ferry is full.");
            Cars.Add(car);
            foreach (var guest in car.Guests)
            {
                AddGuest(guest);
            }
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
            foreach (var guest in car.Guests)
            {
                Guests.Remove(guest);
            }
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = 0;
            total += Cars.Count * PricePerCar;
            total += Guests.Count * PricePerGuest;
            return total;
        }
    }
}