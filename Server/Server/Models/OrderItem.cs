
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;
using Server.Models;

namespace SalernoServer.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public int Count { get; set; } = 1;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Item Item { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public List<OrderItemAddon> Addons { get; set; } = new();
        public List<OrderItemNoOption> NoOptions { get; set; } = new();
        public List<OrderItemGroup> Groups { get; set; } = new();
    }
}