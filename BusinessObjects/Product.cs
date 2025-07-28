using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(40, ErrorMessage = "Product Name cannot exceed 40 characters")]
        [Column("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(20, ErrorMessage = "Quantity Per Unit cannot exceed 20 characters")]
        public string? QuantityPerUnit { get; set; }

        [Column("UnitPrice", TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }

        public int? ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        // Navigation property for OrderDetails
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
} 