using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }

    public enum UserRole
    {
        Customer = 1,
        Staff = 2,
        Manager = 3,
        Admin = 4
    }
} 