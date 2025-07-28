using BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class UserDAO
    {
        private readonly ADNManagermentDbContext _context;

        public UserDAO()
        {
            _context = new ADNManagermentDbContext();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }
        
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                try
                {
                    // Delete related records first
                    var relatedBookings = _context.Bookings.Where(b => b.CustomerId == id).ToList();
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
                    
                    // Delete results where user is staff
                    var staffResults = _context.Results.Where(r => r.StaffId == id).ToList();
                    _context.Results.RemoveRange(staffResults);
                    
                    // Delete feedbacks where user is customer
                    var customerFeedbacks = _context.Feedbacks.Where(f => f.CustomerId == id).ToList();
                    _context.Feedbacks.RemoveRange(customerFeedbacks);
                    
                    // Now delete the user
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting user: {ex.Message}");
                }
            }
        }
    }
} 