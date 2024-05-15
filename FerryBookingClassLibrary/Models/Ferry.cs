using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FerryBookingClassLibrary.Models
{
    public class Ferry
    {
        public int Id { get; set; }

        [Range(10, int.MaxValue, ErrorMessage = "The ferry must have space for at least 10 cars.")]
        public int Length { get; set; }

        [Range(40, int.MaxValue, ErrorMessage = "The ferry must have space for at least 40 guests.")]
        public int MaxGuests { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
