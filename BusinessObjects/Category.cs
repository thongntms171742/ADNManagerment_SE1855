using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(15, ErrorMessage = "Category Name cannot exceed 15 characters")]
        [Column("CategoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Picture")]
        public byte[]? Picture { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
} 