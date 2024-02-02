using System.Text.Json.Serialization;

namespace Server.Models.OrderModels
{
    public class SavedOrderOrderItem : BaseModel
	{
        public long SavedOrderOrderItemId { get; set; }
        [JsonIgnore]
        public SavedOrder? SavedOrder { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
