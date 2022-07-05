using Microsoft.EntityFrameworkCore;
using VehicleAPI.Models;

namespace VehicleAPI.Context
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
