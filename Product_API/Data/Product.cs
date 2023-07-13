using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_API.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        public string Describe { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public byte Discount { get; set; }
        public int? TypeId { get; set; }
        [ForeignKey("TypeId")]
        public Type Type { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }
    }
}
