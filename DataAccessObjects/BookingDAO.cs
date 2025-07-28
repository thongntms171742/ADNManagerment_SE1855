using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookingDAO
    {
        private readonly ADNManagermentDbContext _context;

        public BookingDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .ToList();
        }

        public List<Booking> GetBookingsByCustomerId(int customerId)
        {
            return _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .Include(b => b.Service)
                .ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .FirstOrDefault(b => b.BookingId == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var booking = GetBookingById(id);
            if (booking != null)
            {
                try
                {
                    // Delete related samples
                    var samples = _context.Samples.Where(s => s.BookingId == id).ToList();
                    _context.Samples.RemoveRange(samples);
                    
                    // Delete related results
                    var results = _context.Results.Where(r => r.BookingId == id).ToList();
                    _context.Results.RemoveRange(results);
                    
                    // Delete related feedbacks
                    var feedbacks = _context.Feedbacks.Where(f => f.BookingId == id).ToList();
                    _context.Feedbacks.RemoveRange(feedbacks);
                    
                    // Now delete the booking
                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting booking: {ex.Message}");
                }
            }
        }
    }
} 