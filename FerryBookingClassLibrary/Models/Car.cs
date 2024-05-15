using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerryBookingClassLibrary.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int FerryId { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
