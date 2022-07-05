using BookingAPI.Models;
using System;
using System.Collections.Generic;

namespace BookingAPI.Interfaces
{
    public interface IBookingService
    {
        public List<Booking> GetAllBookings();
        public List<Booking> GetBookingsByDateTime(DateTime start, DateTime end);
        public Booking GetBookingById(int bookingId);
        public bool AddBooking(Booking booking);
        public bool UpdateBooking(Booking booking);
    }
}
