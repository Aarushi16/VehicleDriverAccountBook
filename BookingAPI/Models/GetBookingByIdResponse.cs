namespace BookingAPI.Models
{
    public class GetBookingByIdResponse
    {
        public bool IsSuccess { get; set; }
        public Booking Booking { get; set; }
    }
}
