using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingAPI.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }
        public string Vehicle { get; set; }
        public string Driver { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public double Distance { get; set; }
        public TripType Type { get; set; }
        public double TripFare { get; set; }
        public double FuelExpense { get; set; }
        public double DriverShare { get; set; }
        public string Remarks { get; set; }
    }

    public enum TripType
    {
        Pickup = 1,
        Drop = 2,
        Round = 3,
        Tour = 4
    }
}
