using System.Collections.Generic;
using VehicleAPI.Models;

namespace VehicleAPI.Interfaces
{
    public interface IVehicleService
    {
        public List<Vehicle> Vehicles();
        public List<Vehicle> VehiclesByType(int vehicleType);
        public Vehicle VehicleByRegistrationNo(string registrationNo);
        public bool UpdateVehicle(Vehicle vehicle);
        public bool AddVehicle(Vehicle vehicle);
        public bool DeleteVehicle(string registrationNo);
    }
}
