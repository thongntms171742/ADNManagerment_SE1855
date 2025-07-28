using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ResultDAO
    {
        private readonly ADNManagermentDbContext _context;

        public ResultDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<Result> GetAllResults()
        {
            return _context.Results
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                .Include(r => r.Staff)
                .ToList();
        }

        public Result GetResultById(int id)
        {
            return _context.Results
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                .Include(r => r.Staff)
                .FirstOrDefault(r => r.ResultId == id);
        }

        public Result GetResultByBookingId(int bookingId)
        {
            return _context.Results
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                .Include(r => r.Staff)
                .FirstOrDefault(r => r.BookingId == bookingId);
        }

        public List<Result> GetResultsByStaffId(int staffId)
        {
            return _context.Results
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                .Where(r => r.StaffId == staffId)
                .ToList();
        }

        public List<Result> GetResultsByCustomerId(int customerId)
        {
            return _context.Results
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Customer)
                .Include(r => r.Booking)
                    .ThenInclude(b => b.Service)
                .Include(r => r.Staff)
                .Where(r => r.Booking.CustomerId == customerId)
                .ToList();
        }

        public void AddResult(Result result)
        {
            _context.Results.Add(result);
            _context.SaveChanges();
        }

        public void UpdateResult(Result result)
        {
            _context.Results.Update(result);
            _context.SaveChanges();
        }

        public void DeleteResult(int id)
        {
            var result = GetResultById(id);
            if (result != null)
            {
                _context.Results.Remove(result);
                _context.SaveChanges();
            }
        }
    }
} 