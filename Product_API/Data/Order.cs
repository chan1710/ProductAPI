using System.Data;

namespace Product_API.Data
{
    public enum Check
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Check check { get; set; }
        public string Reciver { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
