using DriversAPI.Context;
using DriversAPI.Interfaces;
using DriversAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace DriversAPI.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly DriverDbContext _context;

        public DriverServices(DriverDbContext context)
        {
            _context = context;
        }

        public List<Driver> Drivers()
        {
            var result = _context.Drivers.ToList();
            return result;
        }

        public List<Driver> DriversByVehicleType(int vehicleType)
        {
            var result = _context.Drivers.Where(x => (VehicleTypes)vehicleType >= x.VehicleType).ToList();
            return result;
        }

        public bool AddDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            var status = _context.SaveChanges();
            if (status == 1)
                return true;
            return false;
        }

        public bool DeleteDriver(string licenseNumber)
        {
            var result = _context.Drivers.Find(licenseNumber);
            if (result == null)
                return false;

            _context.Drivers.Remove(result);
            var status = _context.SaveChanges();
            if (status == 1)
                return true;
            return false;
        }
    }
}
