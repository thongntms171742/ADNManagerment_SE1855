using System;

namespace BusinessObjects
{
    public class Sample
    {
        public int SampleId { get; set; }
        public int BookingId { get; set; }
        public string SampleType { get; set; } = string.Empty; // e.g., Blood, Saliva, Hair
        public DateTime CollectionDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string StorageLocation { get; set; } = string.Empty;

        public virtual Booking Booking { get; set; } = null!;
    }
} 