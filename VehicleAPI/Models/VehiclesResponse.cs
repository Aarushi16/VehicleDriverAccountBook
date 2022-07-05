using System.Collections.Generic;

namespace VehicleAPI.Models
{
    public class VehiclesResponse
    {
        public bool IsSuccess { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
