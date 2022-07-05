using System.Collections.Generic;
using System.Linq;
using VehicleAPI.Context;
using VehicleAPI.Interfaces;
using VehicleAPI.Models;

namespace VehicleAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext _context;

        public VehicleService(VehicleDbContext context)
        {
            _context = context;
        }

        public List<Vehicle> Vehicles()
        {
            var result = _context.Vehicles.ToList();
            return result;
        }

        public List<Vehicle> VehiclesByType(int vehicleType)
        {
            var result = _context.Vehicles.Where(x => x.VehicleType == (VehicleTypes)vehicleType).ToList();
            return result;
        }

        public Vehicle VehicleByRegistrationNo(string registrationNo)
        {
            var result = _context.Vehicles.Find(registrationNo);
            return result;
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            var result = _context.Vehicles.Find(vehicle.RegistrationNo);
            if (result == null)
                return false;

            result.RegistrationNo = vehicle.RegistrationNo;
            result.ModelName = vehicle.ModelName;
            result.VehicleType = vehicle.VehicleType;
            result.NumberOfSeat = vehicle.NumberOfSeat;
            result.AcAvailable = vehicle.AcAvailable;

            var status = _context.SaveChanges();
            if (status == 1)
                return true;
            else
                return false;
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            var status = _context.SaveChanges();
            if (status == 1)
                return true;
            else
                return false;
        }

        public bool DeleteVehicle(string registrationNo)
        {
            var result= _context.Vehicles.Find(registrationNo);
            if (result == null)
                return false;

            _context.Vehicles.Remove(result);
            var status = _context.SaveChanges();
            if (status == 1)
                return true;
            else
                return false;
        }
    }
}
