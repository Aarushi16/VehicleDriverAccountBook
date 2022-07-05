using System.Collections.Generic;

namespace BookingAPI.Models
{
    public class BookingResponse
    {
        public bool IsSuccess { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
