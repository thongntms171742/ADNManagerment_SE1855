using System;

namespace BusinessObjects
{
    public class Result
    {
        public int ResultId { get; set; }
        public int BookingId { get; set; }
        public string ResultDetails { get; set; } = string.Empty; // Could be a link to a PDF or detailed text
        public DateTime ResultDate { get; set; }
        public int StaffId { get; set; } // Staff who uploaded the result
        public string TestStatus { get; set; } = string.Empty; // Completed, In Progress, Failed

        public virtual Booking Booking { get; set; } = null!;
        public virtual User Staff { get; set; } = null!;
    }
} 