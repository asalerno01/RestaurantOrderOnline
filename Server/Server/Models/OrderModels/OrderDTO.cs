using Server.Models.Authentication;

namespace Server.Models.OrderModels
{
    public class OrderDTO
    {
        public long OrderId { get; set; }
        public AccountDTO Account { get; set; }
        public decimal Subtotal { get; set; }
        public decimal SubtotalTax { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string? QuotedDate { get; set; }
        public string? QuotedTime { get; set; }
        public string? PickupDate { get; set; }
        public string? PickupTime { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new();

        public OrderDTO(Order order)
        {
            OrderId = order.OrderId;
            Account = new AccountDTO(order.Account);
            Subtotal = order.Subtotal;
            SubtotalTax = order.SubtotalTax;
            Total = order.Total;
            Status = order.Status;
            OrderDate = order.OrderDate.ToShortDateString();
            OrderTime = order.OrderDate.ToShortTimeString();
            QuotedDate = order.QuotedDate?.ToShortDateString();
            QuotedTime = order.QuotedDate?.ToShortTimeString();
            PickupDate = order.PickupDate?.ToShortDateString();
            PickupTime = order.PickupDate?.ToShortTimeString();
            OrderItems = order.OrderItems.Select(orderItem => new OrderItemDTO(orderItem)).ToList();
        }
    }
}
