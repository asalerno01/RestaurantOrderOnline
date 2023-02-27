using System.Text.Json.Serialization;
using SalernoServer.Models.ItemModels;

namespace SalernoServer.Models
{
    public class OrderItemGroupOption
    {
        [JsonIgnore]
        public OrderItem OrderItem { get; set; }
        public GroupOption GroupOption { get; set; }
    }
}
