using SalernoServer.Models;
using SalernoServer.Models.ModelHelpers;

namespace Server.Models
{
    public class OrderItemDTO
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal BasePrice { get; set; }
        public int Count { get; set; }
        public List<OrderItemAddon> Addons { get; set; }
        public List<OrderItemNoOption> NoOptions { get; set; }
        public List<OrderItemGroup> Groups { get; set; }
        public OrderItemDTO(OrderItem orderItem)
        {
            OrderItemId = orderItem.OrderItemId;
            OrderId = orderItem.Order.OrderId;
            ItemId = orderItem.ItemId;
            ItemName = orderItem.Item.Name;
            BasePrice = orderItem.Item.Price;
            Count = orderItem.Count;
            Addons = orderItem.Addons;
            NoOptions = orderItem.NoOptions;
            Groups = orderItem.Groups;
        }
    }
}
