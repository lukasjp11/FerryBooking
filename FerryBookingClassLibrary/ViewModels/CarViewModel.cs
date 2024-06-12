using System.ComponentModel.DataAnnotations;

namespace FerryBookingClassLibrary.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        public List<int> SelectedGuestIds { get; set; } = new();

        [Required] [Display(Name = "Ferry")] public int FerryId { get; set; }

        public string? FerryName { get; set; }
        public List<string> GuestNames { get; set; } = new();
    }
}