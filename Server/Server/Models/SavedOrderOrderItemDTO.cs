using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderOrderItemDTO
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public List<OrderItemAddon> Addons { get; set; }
        public List<OrderItemNoOption> NoOptions { get; set; }
        public List<OrderItemGroup> Groups { get; set; }
    }
}
