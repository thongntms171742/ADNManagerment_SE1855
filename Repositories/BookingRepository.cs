using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDAO _bookingDAO;

        public BookingRepository()
        {
            _bookingDAO = new BookingDAO();
        }

        public List<Booking> GetAllBookings() => _bookingDAO.GetAllBookings();
        public List<Booking> GetBookingsByCustomerId(int customerId) => _bookingDAO.GetBookingsByCustomerId(customerId);
        public Booking GetBookingById(int id) => _bookingDAO.GetBookingById(id);
        public void AddBooking(Booking booking) => _bookingDAO.AddBooking(booking);
        public void UpdateBooking(Booking booking) => _bookingDAO.UpdateBooking(booking);
        public void DeleteBooking(int id) => _bookingDAO.DeleteBooking(id);
    }
} 