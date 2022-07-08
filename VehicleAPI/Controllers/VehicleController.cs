using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using VehicleAPI.Interfaces;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
        {
            _service = service;
        }

        public VehicleController()
        {
        }

        [HttpGet]
        public IActionResult Vehicles()
        {
            VehiclesResponse response = new VehiclesResponse();
            response.IsSuccess = true;
            response.Vehicles = _service.Vehicles();
            return Ok(response);
        }

        [HttpGet]
        public IActionResult VehiclesByType(int vehicleType)
        {
            VehiclesResponse response = new VehiclesResponse();
            response.IsSuccess = true;
            response.Vehicles = _service.VehiclesByType(vehicleType);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult VehicleByRegistrationNo(string registrationNo)
        {
            VehicleByRegistrationNoResponse response = new VehicleByRegistrationNoResponse();
            response.IsSuccess = true;
            response.Vehicle = _service.VehicleByRegistrationNo(registrationNo);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateVehicle(Vehicle vehicle)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _service.UpdateVehicle(vehicle);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddVehicle(Vehicle vehicle)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _service.AddVehicle(vehicle);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult DeleteVehicle(string registrationNo)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _service.DeleteVehicle(registrationNo);
            return Ok(response);
        }

        public object Get(object response)
        {
            throw new NotImplementedException();
        }
    }
}
