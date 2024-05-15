namespace FerryBookingClassLibrary.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int? CarId { get; set; }
        public Car Car { get; set; }
        public int FerryId { get; set; }
        public Ferry Ferry { get; set; }
    }
}