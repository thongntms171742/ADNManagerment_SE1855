using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class OrderDetail
    {
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }

        [Key, Column(Order = 2)]
        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public short Quantity { get; set; }

        [Required]
        public float Discount { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;

        [NotMapped]
        public decimal Total
        {
            get { return UnitPrice * Quantity * (1 - (decimal)Discount); }
        }

        // Helper property to get discount as percentage for display
        [NotMapped]
        public decimal DiscountPercentage
        {
            get { return (decimal)Discount * 100; }
        }
    }
} 