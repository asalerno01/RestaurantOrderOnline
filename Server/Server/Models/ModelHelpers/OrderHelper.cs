using SalernoServer.Models.ModelHelpers;

namespace SalernoServer.Models
{
    public class OrderHelper
    {
        public long OrderId { get; set; } = 0;
        public long? AccountId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public decimal Subtotal { get; set; } = 0;
        public decimal SubtotalTax { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; }
        public DateTime? PickupDate { get; set; } = null;
        public string? SavedOrderName { get; set; } = null;
        public bool SaveOrder { get; set; } = false;
        public List<OrderItemHelper> OrderItems { get; set; }
    }
}
