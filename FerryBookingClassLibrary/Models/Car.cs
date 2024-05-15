using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerryBookingClassLibrary.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The car must have at least 1 guest.")]
        [MaxLength(5, ErrorMessage = "The car can have a maximum of 5 guests.")]
        public List<Guest> Guests { get; set; } = new List<Guest>();

        [Required]
        public int FerryId { get; set; }

        public Ferry Ferry { get; set; }

        public void AddGuest(Guest guest)
        {
            if (Guests.Count >= 5)
                throw new InvalidOperationException("Cannot add more guests to the car, maximum limit reached.");
            Guests.Add(guest);
        }

        public void RemoveGuest(Guest guest)
        {
            Guests.Remove(guest);
        }
    }
}