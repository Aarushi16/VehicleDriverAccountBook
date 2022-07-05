using DriversAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DriversAPI.Context
{
    public class DriverDbContext : DbContext
    {
        public DriverDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Driver> Drivers { get; set; }
    }
}
