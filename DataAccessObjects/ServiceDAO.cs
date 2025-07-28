using BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ServiceDAO
    {
        private readonly ADNManagermentDbContext _context;

        public ServiceDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<Service> GetAllServices()
        {
            return _context.Services.ToList();
        }

        public Service GetServiceById(int id)
        {
            return _context.Services.FirstOrDefault(s => s.ServiceId == id);
        }

        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void UpdateService(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
        }

        public void DeleteService(int id)
        {
            var service = GetServiceById(id);
            if (service != null)
            {
                try
                {
                    // Delete related bookings first
                    var relatedBookings = _context.Bookings.Where(b => b.ServiceId == id).ToList();
                    foreach (var booking in relatedBookings)
                    {
                        // Delete related samples
                        var samples = _context.Samples.Where(s => s.BookingId == booking.BookingId).ToList();
                        _context.Samples.RemoveRange(samples);
                        
                        // Delete related results
                        var results = _context.Results.Where(r => r.BookingId == booking.BookingId).ToList();
                        _context.Results.RemoveRange(results);
                        
                        // Delete related feedbacks
                        var feedbacks = _context.Feedbacks.Where(f => f.BookingId == booking.BookingId).ToList();
                        _context.Feedbacks.RemoveRange(feedbacks);
                    }
                    _context.Bookings.RemoveRange(relatedBookings);
                    
                    // Now delete the service
                    _context.Services.Remove(service);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting service: {ex.Message}");
                }
            }
        }
    }
} 