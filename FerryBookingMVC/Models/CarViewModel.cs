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
    }
}