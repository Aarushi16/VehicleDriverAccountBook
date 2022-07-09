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
            BookingResponse response = new BookingResponse
            {
                IsSuccess = true,
                Bookings = _bookingService.GetAllBookings()
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetBookingsByDateTime(DateTime start, DateTime end)
        {
            BookingResponse response = new BookingResponse
            {
                IsSuccess = true,
                Bookings = _bookingService.GetBookingsByDateTime(start, end)
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetBookingById(int bookingId)
        {
            GetBookingByIdResponse getBookingByIdResponse3 = new GetBookingByIdResponse
            {
                IsSuccess = true,
                Booking = _bookingService.GetBookingById(bookingId)
            };
            GetBookingByIdResponse getBookingByIdResponse2 = getBookingByIdResponse3;
            GetBookingByIdResponse getBookingByIdResponse1 = getBookingByIdResponse2;
            GetBookingByIdResponse getBookingByIdResponse = getBookingByIdResponse1;
            GetBookingByIdResponse response = getBookingByIdResponse;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddBooking(Booking booking)
        {
            ResponseStatus responseStatus = new ResponseStatus
            {
                IsSuccess = _bookingService.AddBooking(booking)
            };
            ResponseStatus response = responseStatus;

            return Ok(response);
        }

        [HttpPut]
        public IActionResult UpdateBooking(Booking booking)
        {
            ResponseStatus response = new ResponseStatus
            {
                IsSuccess = _bookingService.UpdateBooking(booking)
            };

            return Ok(response);
        }
    }
}
