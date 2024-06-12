using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FerryBookingClassLibrary.Models
{
    public class Ferry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Max Cars must be a non-negative number.")]
        public int MaxCars { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Max Guests must be a non-negative number.")]
        public int MaxGuests { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price per Car must be a non-negative number.")]
        public int PricePerGuest { get; set; } = 99;

        [Range(0, int.MaxValue, ErrorMessage = "Price per Guest must be a non-negative number.")]
        public int PricePerCar { get; set; } = 197;

        [JsonIgnore] public List<Car> Cars { get; set; } = new();

        public List<Guest> Guests { get; set; } = new();

        public int CalculateTotalPrice()
        {
            int total = 0;
            total += Cars.Count * PricePerCar;
            total += Guests.Count * PricePerGuest;
            return total;
        }
    }
}