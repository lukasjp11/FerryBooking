using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FerryBookingMVC.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        public List<int> SelectedGuestIds { get; set; } = new List<int>();

        [Required]
        [Display(Name = "Ferry")]
        public int FerryId { get; set; }

        // Add properties for display purposes if needed
        public string FerryName { get; set; }
        public List<string> GuestNames { get; set; } = new List<string>();
    }
}

