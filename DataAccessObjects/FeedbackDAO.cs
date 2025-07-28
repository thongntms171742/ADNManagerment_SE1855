using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class FeedbackDAO
    {
        private readonly ADNManagermentDbContext _context;

        public FeedbackDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Service)
                .ToList();
        }

        public List<Feedback> GetFeedbacksByCustomerId(int customerId)
        {
            return _context.Feedbacks
                .Where(f => f.CustomerId == customerId)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Service)
                .ToList();
        }

        public Feedback? GetFeedbackById(int id)
        {
            return _context.Feedbacks
                .Include(f => f.Customer)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Service)
                .FirstOrDefault(f => f.FeedbackId == id);
        }

        public List<Feedback> GetFeedbacksByBookingId(int bookingId)
        {
            return _context.Feedbacks
                .Where(f => f.BookingId == bookingId)
                .Include(f => f.Customer)
                .Include(f => f.Booking)
                .ThenInclude(b => b.Service)
                .ToList();
        }

        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            _context.SaveChanges();
        }

        public void DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
            }
        }
    }
} 