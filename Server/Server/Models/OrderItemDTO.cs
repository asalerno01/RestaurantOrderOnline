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
    }
}
