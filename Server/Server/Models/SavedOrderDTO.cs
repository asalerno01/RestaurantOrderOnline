using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderDTO
    {
        public string SavedOrderName { get; set; }
        public long CustomerAccountId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<SavedOrderOrderItemDTO> OrderItems { get; set; }
    }
}
