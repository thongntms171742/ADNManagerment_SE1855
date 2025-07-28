using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(40, ErrorMessage = "Company Name cannot exceed 40 characters")]
        [Column("CompanyName")]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Contact Name cannot exceed 30 characters")]
        [Column("ContactName")]
        public string? ContactName { get; set; }

        [StringLength(30, ErrorMessage = "Contact Title cannot exceed 30 characters")]
        [Column("ContactTitle")]
        public string? ContactTitle { get; set; }

        [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters")]
        public string? Address { get; set; }

        [StringLength(24, ErrorMessage = "Phone cannot exceed 24 characters")]
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = string.Empty;

        // Navigation property for Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
} 