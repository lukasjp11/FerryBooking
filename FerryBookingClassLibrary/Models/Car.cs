using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public Ferry? Ferry { get; set; }
    }
}