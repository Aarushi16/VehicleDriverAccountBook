using DriversAPI.Models;
using System.Collections.Generic;

namespace DriversAPI.Interfaces
{
    public interface IDriverServices
    {
        public List<Driver> Drivers();
        public List<Driver> DriversByVehicleType(int vehicleType);
        public bool AddDriver(Driver driver);
        public bool DeleteDriver(string licenseNumber);
    }
}
