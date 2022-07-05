using System.ComponentModel.DataAnnotations;

namespace DriversAPI.Models
{
    public class Driver
    {
        [Key]
        public string LicenseNumber { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public VehicleTypes VehicleType { get; set; }
    }

    public enum VehicleTypes
    {
        Sedan = 1,
        SUV = 2,
        Van = 3
    }
}
