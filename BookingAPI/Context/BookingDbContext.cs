using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Context
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().Property(p => p.BookingID).ValueGeneratedOnAdd();
        }
    }
}
