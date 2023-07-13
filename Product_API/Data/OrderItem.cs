namespace Product_API.Data
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int SoLuong { get; set; }
        public double Price { get; set; }
        public byte Discount { get; set; }

        //relation
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
