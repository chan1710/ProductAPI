using System.ComponentModel.DataAnnotations;

namespace Product_API.Models
{
    public class TypeModel
    {
        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }
    }
}
