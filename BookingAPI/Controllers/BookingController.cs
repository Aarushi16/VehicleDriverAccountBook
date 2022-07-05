using BookingAPI.Interfaces;
using BookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace BookingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            //var accessToken = Request.Headers[HeaderNames.Authorization].ToString();
            BookingResponse response = new BookingResponse();
            response.IsSuccess = true;
            response.Bookings = _bookingService.GetAllBookings();

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetBookingsByDateTime(DateTime start, DateTime end)
        {
            BookingResponse response = new BookingResponse();
            response.IsSuccess = true;
            response.Bookings = _bookingService.GetBookingsByDateTime(start, end);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetBookingById(int bookingId)
        {
            GetBookingByIdResponse response = new GetBookingByIdResponse();
            response.IsSuccess = true;
            response.Booking = _bookingService.GetBookingById(bookingId);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddBooking(Booking booking)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _bookingService.AddBooking(booking);

            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateBooking(Booking booking)
        {
            ResponseStatus response = new ResponseStatus();
            response.IsSuccess = _bookingService.UpdateBooking(booking);

            return Ok(response);
        }
    }
}
