using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_API.Data
{
    [Table("Type")]
    public class Type
    {
        [Key]
        public int TypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
