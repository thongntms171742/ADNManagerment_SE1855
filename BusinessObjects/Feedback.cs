using System;

namespace BusinessObjects
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
        public int Rating { get; set; } // e.g., 1-5 stars
        public string Comment { get; set; } = string.Empty;
        public DateTime FeedbackDate { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual Booking Booking { get; set; } = null!;
    }
} 