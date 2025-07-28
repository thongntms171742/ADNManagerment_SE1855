using BusinessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByCustomerId(int customerId);
        Booking GetBookingById(int id);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
        void UpdateBookingStatus(int bookingId, BookingStatus status);
    }
} 