using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
} 