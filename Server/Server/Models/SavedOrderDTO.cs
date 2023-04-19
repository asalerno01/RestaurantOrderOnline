using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderDTO
    {
        public string SavedOrderName { get; set; }
        //public long AccountId { get; set; }
        public DateTime LastOrderDate { get; set; }
        public List<SavedOrderOrderItemDTO> OrderItems { get; set; }
    }
}
