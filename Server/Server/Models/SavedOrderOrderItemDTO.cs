using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderOrderItemDTO
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public List<OrderItemAddon> Addons { get; set; } = new();
        public List<OrderItemNoOption> NoOptions { get; set; } = new();
        public List<OrderItemGroup> Groups { get; set; } = new();

        public SavedOrderOrderItemDTO(SavedOrderOrderItem orderItem)
        {
            ItemId = orderItem.OrderItem.Item.ItemId;
            Name = orderItem.OrderItem.Item.Name;
            Price = orderItem.OrderItem.Item.Price;
            Description = orderItem.OrderItem.Item.Description;
            Count = orderItem.OrderItem.Count;
            Addons = orderItem.OrderItem.Addons;
            NoOptions = orderItem.OrderItem.NoOptions;
            Groups = orderItem.OrderItem.Groups;
        }
    }
}
