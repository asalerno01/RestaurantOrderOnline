using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.Helpers
{
    public class CategoryItemDTO
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
        // TODO: Add modifiers
        public CategoryItemDTO(string itemId, string name, string description, decimal price, bool isEnabled)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
            Price = price;
            IsEnabled = isEnabled;
        }
    }
}
