
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;
using Server.Models;

namespace SalernoServer.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }
        public List<OrderItemAddon> OrderItemAddons { get; set; } = new();
        public List<OrderItemNoOption> OrderItemNoOptions { get; set; } = new();
        public List<OrderItemGroup> OrderItemGroups { get; set; } = new();

    }
}