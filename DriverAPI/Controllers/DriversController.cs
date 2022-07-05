using DriversAPI.Interfaces;
using DriversAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriversAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class DriversController : ControllerBase
    {
        private readonly IDriverServices _driverServices;

        public DriversController(IDriverServices driverServices)
        {
            _driverServices = driverServices;
        }

        [HttpGet]
        public IActionResult Drivers()
        {
            DriversResponse response = new DriversResponse();
            response.IsSuccess = true;
            response.Drivers = _driverServices.Drivers();

            return Ok(response);
        }

        [HttpGet]
        public IActionResult DriversByVehicleType(int vehicleType)
        {
            DriversResponse response = new DriversResponse();
            response.IsSuccess = true;
            response.Drivers = _driverServices.DriversByVehicleType(vehicleType);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddDriver(Driver driver)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _driverServices.AddDriver(driver);

            return Ok(response);
        }

        [HttpDelete]
        public IActionResult DeleteDriver(string licenseNumber)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _driverServices.DeleteDriver(licenseNumber);

            return Ok(response);
        }
    }
}
