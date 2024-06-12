using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FerryBookingClassLibrary.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required] public bool Gender { get; set; }

        [Required] public int FerryId { get; set; }

        [JsonIgnore] public Ferry? Ferry { get; set; }
    }
}