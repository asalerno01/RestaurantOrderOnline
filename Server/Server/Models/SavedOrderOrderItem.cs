using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderOrderItem
    {
        public long SavedOrderOrderItemId { get; set; }
        public SavedOrder SavedOrder { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
