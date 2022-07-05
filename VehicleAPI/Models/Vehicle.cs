using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Models
{
    public class Vehicle
    {
        [Key]
        public string RegistrationNo { get; set; }
        public string ModelName { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public int NumberOfSeat { get; set; }
        public ACTypes AcAvailable { get; set; }
    }

    public enum VehicleTypes
    {
        Sedan = 1,
        SUV = 2,
        Van = 3
    }

    public enum ACTypes
    {
        No = 0,
        Yes = 1
    }
}
