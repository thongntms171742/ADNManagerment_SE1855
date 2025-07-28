using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class SampleDAO
    {
        private readonly ADNManagermentDbContext _context;

        public SampleDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<Sample> GetAllSamples()
        {
            return _context.Samples
                .Include(s => s.Booking)
                .ToList();
        }

        public Sample GetSampleById(int id)
        {
            return _context.Samples
                .Include(s => s.Booking)
                .FirstOrDefault(s => s.SampleId == id);
        }

        public Sample GetSampleByBookingId(int bookingId)
        {
            return _context.Samples
                .Include(s => s.Booking)
                .FirstOrDefault(s => s.BookingId == bookingId);
        }

        public void AddSample(Sample sample)
        {
            _context.Samples.Add(sample);
            _context.SaveChanges();
        }

        public void UpdateSample(Sample sample)
        {
            _context.Samples.Update(sample);
            _context.SaveChanges();
        }

        public void DeleteSample(int id)
        {
            var sample = GetSampleById(id);
            if (sample != null)
            {
                _context.Samples.Remove(sample);
                _context.SaveChanges();
            }
        }
    }
} 