using System;

namespace BusinessObjects
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public CollectionMethod CollectionMethod { get; set; }
        public string? ShippingAddress { get; set; } // For home collection
        public DateTime CreatedDate { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
        public virtual Sample? Sample { get; set; }
        public virtual Result? Result { get; set; }
    }

    public enum BookingStatus
    {
        Registered,
        KitSent,
        SampleCollected,
        SampleReceived,
        Testing,
        Completed,
        Cancelled
    }

    public enum CollectionMethod
    {
        SelfCollection,
        HomeCollection,
        OnSiteCollection
    }
} 