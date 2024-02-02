
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models.ItemModels;
using Server.Models;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.OrderModels
{
    public class OrderItem : BaseModel
	{
        public long OrderItemId { get; set; }
        public int Count { get; set; } = 1;
        public ItemSnapshot Item { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public List<OrderItemAddon> Addons { get; set; } = new();
        public List<OrderItemNoOption> NoOptions { get; set; } = new();
        public List<OrderItemGroup> Groups { get; set; } = new();
    }
}