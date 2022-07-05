using BookingAPI.Context;
using BookingAPI.Interfaces;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingDbContext _context;

        public BookingService(BookingDbContext context)
        {
            _context = context;
        }


        public List<Booking> GetAllBookings()
        {
            var result = _context.Bookings.ToList();
            return result;
        }

        public List<Booking> GetBookingsByDateTime(DateTime start, DateTime end)
        {
            var result = _context.Bookings.Where(x => start <= x.Start && end >= x.End).ToList();
            return result;
        }

        public Booking GetBookingById(int bookingId)
        {
            var result = _context.Bookings.Where(x => x.BookingID == bookingId).First();
            return result;
        }

        public bool AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateBooking(Booking booking)
        {
            var result = _context.Bookings.Find(booking.BookingID);
            if (result == null)
                return false;

            result.Vehicle = booking.Vehicle;
            result.Driver = booking.Driver;
            result.Start = booking.Start;
            result.End = booking.End;
            result.FromLocation = booking.FromLocation;
            result.ToLocation = booking.ToLocation;
            result.Distance = booking.Distance;
            result.Type = booking.Type;
            result.TripFare = booking.TripFare;
            result.FuelExpense = booking.FuelExpense;
            result.DriverShare = booking.DriverShare;
            result.Remarks = booking.Remarks;

            _context.SaveChanges();
            return true;
        }
    }
}
