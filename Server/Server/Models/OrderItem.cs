
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;

namespace SalernoServer.Models
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }
        public decimal Subtotal { get; set; } = 0;
        public List<OrderItemAddon> OrderItemAddons { get; set; } = new();
        public List<OrderItemNoOption> OrderItemNoOptions { get; set; } = new();
        public List<OrderItemGroupOption> OrderItemGroupOptions { get; set; } = new();

    }
}