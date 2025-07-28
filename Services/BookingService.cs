using BusinessObjects;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }

        public List<Booking> GetAllBookings() => _bookingRepository.GetAllBookings();
        public List<Booking> GetBookingsByCustomerId(int customerId) => _bookingRepository.GetBookingsByCustomerId(customerId);
        public Booking GetBookingById(int id) => _bookingRepository.GetBookingById(id);
        public void AddBooking(Booking booking) => _bookingRepository.AddBooking(booking);
        public void UpdateBooking(Booking booking) => _bookingRepository.UpdateBooking(booking);
        public void DeleteBooking(int id) => _bookingRepository.DeleteBooking(id);

        public void UpdateBookingStatus(int bookingId, BookingStatus status)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                _bookingRepository.UpdateBooking(booking);
            }
        }
    }
} 