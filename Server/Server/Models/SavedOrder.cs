using SalernoServer.Models;
using Server.Models.Authentication;

namespace Server.Models
{
    public class SavedOrder
    {
        public long SavedOrderId { get; set; }
        public string Name { get; set; }
        public Account Account { get; set; }
        public DateTime LastOrderDate { get; set; } = DateTime.Now;
        public List<SavedOrderOrderItem> OrderItems { get; set; } = new();
    }
}