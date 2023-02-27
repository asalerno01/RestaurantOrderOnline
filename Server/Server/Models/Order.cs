namespace SalernoServer.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public decimal Subtotal { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal Net { get; set; } = 0;
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
