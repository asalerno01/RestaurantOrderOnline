using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderDTO
    {
        public long SavedOrderId { get; set; }
        public string Name { get; set; }
        public DateTime LastOrderDate { get; set; }
        public List<SavedOrderOrderItemDTO> OrderItems { get; set; } = new();

        public SavedOrderDTO(SavedOrder savedOrder)
        {
            SavedOrderId = savedOrder.SavedOrderId;
            Name = savedOrder.Name;
            LastOrderDate = savedOrder.LastOrderDate;
            OrderItems = savedOrder.OrderItems.Select(orderItem => new SavedOrderOrderItemDTO(orderItem)).ToList();
        }
    }
}
