using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, ErrorMessage = "Username cannot exceed 30 characters")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Password cannot exceed 30 characters")]
        public string Password { get; set; } = string.Empty;
        
        [StringLength(30, ErrorMessage = "Job Title cannot exceed 30 characters")]
        public string? JobTitle { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters")]
        public string? Address { get; set; }

        // Navigation property for Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
