using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.Helpers
{
    public class CategoryItemDTO
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Modifier Modifier { get; set; }
        public CategoryItemDTO(string itemId, string name, string description, decimal price, Modifier modifier)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
            Price = price;
            Modifier = modifier;
        }
    }
}
