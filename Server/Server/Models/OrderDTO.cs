using SalernoServer.Models;
using Server.Models.Authentication;

namespace Server.Models
{
    public class OrderDTO
    {
        public long OrderId { get; set; }
        public OrderAccountDTO? Account { get; set; }
        public decimal Subtotal { get; set; } = 0;
        public decimal SubtotalTax { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? PickupDate { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
