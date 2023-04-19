using SalernoServer.Models;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class SavedOrderOrderItem
    {
        [JsonIgnore]
        public long SavedOrderOrderItemId { get; set; }
        [JsonIgnore]
        public SavedOrder SavedOrder { get; set; }
        public OrderItem OrderItem { get; set; }
}
    }
