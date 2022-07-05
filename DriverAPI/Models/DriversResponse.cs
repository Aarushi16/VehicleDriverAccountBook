using System.Collections.Generic;

namespace DriversAPI.Models
{
    public class DriversResponse
    {
        public bool IsSuccess { get; set; }
        public List<Driver> Drivers { get; set; }
    }
}
