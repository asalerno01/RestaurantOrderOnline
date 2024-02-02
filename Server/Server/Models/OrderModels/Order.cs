using Microsoft.EntityFrameworkCore;
using Server.Models.Authentication;

namespace Server.Models.OrderModels
{
    public class Order : BaseModel
    {
        public long OrderId { get; set; }
        public Account Account { get; set; }
        [Precision(18, 2)]
        public decimal Subtotal { get; set; } = 0;
        [Precision(18, 2)]
        public decimal SubtotalTax { get; set; } = 0;
        [Precision(18, 2)]
        public decimal Total { get; set; } = 0;
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? QuotedDate { get; set; } = null;
        public DateTime? PickupDate { get; set; } = null;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
