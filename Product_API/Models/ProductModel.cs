namespace Product_API.Models
{
    public class ProductVM
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
    }

    public class Product : ProductVM
    {
        public Guid ProductId { get; set; }
    }

    public class ProductModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string TypeName { get; set; }
    }
}
