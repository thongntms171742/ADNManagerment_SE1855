using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        List<Booking> GetBookingsByCustomerId(int customerId);
        Booking GetBookingById(int id);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
    }
} 